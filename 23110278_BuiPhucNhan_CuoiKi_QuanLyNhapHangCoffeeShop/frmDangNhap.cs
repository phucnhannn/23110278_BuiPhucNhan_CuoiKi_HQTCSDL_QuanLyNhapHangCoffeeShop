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
    public partial class frmDangNhap : Form
    {
        string strCon;
        string username;
        string password;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            username = txtTenDangNhap.Text;
            password = txtMatKhau.Text;
            strCon = $"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID={username};Password={password};TrustServerCertificate=True";
            try 
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    this.Hide();
                    frmMenu menu = new frmMenu(username, password);
                    menu.ShowDialog();
                    this.Show();
                }
            }
            catch
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
