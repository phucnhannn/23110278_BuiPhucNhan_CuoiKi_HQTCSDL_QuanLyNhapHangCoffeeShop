using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace _23110278_BuiPhucNhan_CuoiKi_QuanLyNhapHangCoffeeShop
{
    public partial class frmNhaCungCap : Form
    {
        string strCon;
        public frmNhaCungCap(string strCon)
        {
            InitializeComponent();
            this.strCon = strCon;
            ShowNhaCungCap();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {

        }
        private void ShowNhaCungCap()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand("sp_ShowNhaCungCap", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhaCungCap.DataSource = dt;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQLNhaCungCap qlncc = new frmQLNhaCungCap(strCon);
            qlncc.Text = "Thêm nhà cung cấp mới";
            qlncc.ShowDialog();
            this.Show();
            ShowNhaCungCap();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                int supplierId = Convert.ToInt32(dgvNhaCungCap.CurrentRow.Cells["Mã nhà cung cấp"].Value);
                string name = dgvNhaCungCap.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();
                string address = dgvNhaCungCap.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                string phone = dgvNhaCungCap.CurrentRow.Cells["Số điện thoại"].Value.ToString();
                string email = dgvNhaCungCap.CurrentRow.Cells["Email"].Value.ToString();

                frmQLNhaCungCap frm = new frmQLNhaCungCap(supplierId, name, address, phone, email, strCon);
                frm.Text = "Sửa thông tin nhà cung cấp";
                frm.ShowDialog();
                ShowNhaCungCap();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                int supplierId = Convert.ToInt32(dgvNhaCungCap.CurrentRow.Cells["Mã nhà cung cấp"].Value);
                string name = dgvNhaCungCap.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhà cung cấp \"{name}\"?",
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
                            SqlCommand cmd = new SqlCommand("sp_XoaNhaCungCap", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowNhaCungCap();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show(
                                "Không thể xóa nhà cung cấp này vì đang được sử dụng trong đơn đặt hàng!",
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

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ShowNhaCungCap();
            MessageBox.Show("Đã làm mới dữ liệu nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNhaCungCap = txtTimKiem.Text.Trim();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM dbo.fn_TimKiemNhaCungCap(@TenNhaCungCap)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenNhaCungCap", tenNhaCungCap);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhaCungCap.DataSource = dt;
            }
        }
    }
}
