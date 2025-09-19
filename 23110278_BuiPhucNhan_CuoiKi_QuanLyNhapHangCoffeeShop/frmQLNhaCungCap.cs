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
    public partial class frmQLNhaCungCap : Form
    {
        private int supplierId = -1;
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmQLNhaCungCap()
        {
            InitializeComponent();
        }
        public frmQLNhaCungCap(int id, string name, string address, string phone, string email)
        {
            InitializeComponent();
            supplierId = id;
            txtTenNhaCungCap.Text = name;
            txtDiaChi.Text = address;
            txtSDT.Text = phone;
            txtEmail.Text = email;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd;
                if (supplierId == -1)
                {
                    cmd = new SqlCommand("sp_ThemNhaCungCap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierName", txtTenNhaCungCap.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                }
                else
                {
                    cmd = new SqlCommand("sp_SuaNhaCungCap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmd.Parameters.AddWithValue("@SupplierName", txtTenNhaCungCap.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Lưu thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmQLNhaCungCap_Load(object sender, EventArgs e)
        {

        }
    }
}
