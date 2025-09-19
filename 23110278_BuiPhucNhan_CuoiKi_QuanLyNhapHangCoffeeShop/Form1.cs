using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _23110278_BuiPhucNhan_CuoiKi_QuanLyNhapHangCoffeeShop
{
    public partial class frmMenu : Form
    {
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmMenu()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnQLNhaCungCap_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNhaCungCap nhacungcap = new frmNhaCungCap();
            nhacungcap.ShowDialog();
            this.Show();
        }
    }
}
