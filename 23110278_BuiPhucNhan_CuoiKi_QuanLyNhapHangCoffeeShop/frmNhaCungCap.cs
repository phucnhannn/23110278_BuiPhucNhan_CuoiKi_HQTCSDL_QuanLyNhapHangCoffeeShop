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
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmNhaCungCap()
        {
            InitializeComponent();
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
            frmQLNhaCungCap qlncc = new frmQLNhaCungCap();
            qlncc.Text = "Thêm nhà cung cấp mới";
            qlncc.ShowDialog();
            this.Show();
        }
    }
}
