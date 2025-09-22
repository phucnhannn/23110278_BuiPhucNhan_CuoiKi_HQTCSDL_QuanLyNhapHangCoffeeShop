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
        private string currentUser;
        private string userRole;
        string strCon;
        string username;
        string password;
        public frmMenu()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }
        public frmMenu(string user, string pass) : this()
        {
            username = user;
            password = pass;
            strCon = $"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID={username};Password={password};TrustServerCertificate=True";
            currentUser = username;
            DetermineUserRole();
            ApplyRolePermissions();
        }

        private void DetermineUserRole()
        {
            if (currentUser.ToLower() == "nvnh")
            {
                userRole = "NhanVienNhapHang";
            }
            if (currentUser.ToLower() == "nvk")
            {
                userRole = "NhanVienKho";
            }
        }

        private void ApplyRolePermissions()
        {
            if (userRole == "NhanVienNhapHang")
            {
                btnTaoPhieuXuatKho.Enabled = false;
                btnQLXuatKho.Enabled = false;
                btnThongKeKho.Enabled = false;
                bthQuanLyNguyenLieu.Enabled = false;

                btnTaoPhieuXuatKho.BackColor = Color.Gray;
                btnQLXuatKho.BackColor = Color.Gray;
                btnThongKeKho.BackColor = Color.Gray;
                bthQuanLyNguyenLieu.BackColor = Color.Gray;
            }

            if (userRole == "NhanVienKho")
            {
                btnTaoPhieuNhapHang.Enabled = false;
                btnQLNhapHang.Enabled = false;
                btnQLNhaCungCap.Enabled = false;

                btnTaoPhieuNhapHang.BackColor = Color.Gray;
                btnQLNhapHang.BackColor = Color.Gray;
                btnQLNhaCungCap.BackColor = Color.Gray;
            }
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnQLNhaCungCap_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNhaCungCap nhacungcap = new frmNhaCungCap(strCon);
            nhacungcap.ShowDialog();
            this.Show();
        }

        private void bthQuanLyNguyenLieu_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNguyenLieu nguyenlieu = new frmNguyenLieu(strCon);
            nguyenlieu.ShowDialog();
            this.Show();
        }

        private void btnTaoPhieuNhapHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhieuNhapHang phieunhaphang = new frmPhieuNhapHang(strCon);
            phieunhaphang.ShowDialog();
            this.Show();
        }

        private void btnQLNhapHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQLNhapHang qlnhaphang = new frmQLNhapHang(strCon);
            qlnhaphang.ShowDialog();
            this.Show();
        }

        private void btnQLXuatKho_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQLXuatKho qlxuatkho = new frmQLXuatKho(strCon);
            qlxuatkho.ShowDialog();
            this.Show();

        }

        private void btnTaoPhieuXuatKho_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPhieuXuatKho phieuxuatkho = new frmPhieuXuatKho(strCon);
            phieuxuatkho.ShowDialog();
            this.Show();
        }

        private void btnThongKeKho_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmQLKho quanlykho = new frmQLKho(strCon);
            quanlykho.ShowDialog();
            this.Show();
        }
    }
}
