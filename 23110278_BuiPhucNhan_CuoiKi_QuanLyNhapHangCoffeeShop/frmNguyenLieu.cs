using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _23110278_BuiPhucNhan_CuoiKi_QuanLyNhapHangCoffeeShop
{
    public partial class frmNguyenLieu : Form
    {
        string strCon;
        public frmNguyenLieu(string strCon)
        {
            InitializeComponent();
            this.strCon = strCon;
            ShowNguyenLieu();
        }

        private void frmNguyenLieu_Load(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ShowNguyenLieu()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand("sp_ShowNguyenLieu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNguyenLieu.DataSource = dt;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmQLNguyenLieu frm = new frmQLNguyenLieu(strCon);
            frm.Text = "Thêm nguyên liệu mới";
            frm.ShowDialog();
            ShowNguyenLieu();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNguyenLieu.CurrentRow != null)
            {
                int materialId = Convert.ToInt32(dgvNguyenLieu.CurrentRow.Cells["Mã nguyên liệu"].Value);
                string name = dgvNguyenLieu.CurrentRow.Cells["Tên nguyên liệu"].Value.ToString();
                string unit = dgvNguyenLieu.CurrentRow.Cells["Đơn vị tính"].Value?.ToString();
                int reorderLevel = dgvNguyenLieu.CurrentRow.Cells["Lượng tồn tối thiểu"].Value != DBNull.Value
                    ? Convert.ToInt32(dgvNguyenLieu.CurrentRow.Cells["Lượng tồn tối thiểu"].Value)
                    : 0;

                frmQLNguyenLieu frm = new frmQLNguyenLieu(materialId, name, unit, reorderLevel);
                frm.Text = "Sửa thông tin nguyên liệu";
                frm.ShowDialog();
                ShowNguyenLieu();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNguyenLieu.CurrentRow != null)
            {
                int materialId = Convert.ToInt32(dgvNguyenLieu.CurrentRow.Cells["Mã nguyên liệu"].Value);
                string name = dgvNguyenLieu.CurrentRow.Cells["Tên nguyên liệu"].Value.ToString();

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nguyên liệu \"{name}\"?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strCon))
                        {
                            SqlCommand cmd = new SqlCommand("sp_XoaNguyenLieu", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaterialID", materialId);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        MessageBox.Show("Xóa nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowNguyenLieu();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show(
                                "Không thể xóa nguyên liệu này vì đang được sử dụng trong phiếu nhập/xuất kho!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "Có lỗi xảy ra: " + ex.Message,
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ShowNguyenLieu();
            MessageBox.Show("Đã làm mới dữ liệu nguyên liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNguyenLieu = txtTimKiem.Text.Trim();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM dbo.fn_TimKiemNguyenLieu(@TenNguyenLieu)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNguyenLieu.DataSource = dt;
            }
        }
    }
}
