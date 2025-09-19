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
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmNguyenLieu()
        {
            InitializeComponent();
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
            frmQLNguyenLieu frm = new frmQLNguyenLieu();
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
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
