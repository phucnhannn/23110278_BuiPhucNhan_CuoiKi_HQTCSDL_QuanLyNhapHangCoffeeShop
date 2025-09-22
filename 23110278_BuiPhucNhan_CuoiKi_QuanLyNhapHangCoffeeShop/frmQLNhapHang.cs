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
    public partial class frmQLNhapHang : Form
    {
        string strCon;
        private void LoadNhapHang()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM view_QuanLyNhapHang";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQLNhapHang.DataSource = dt;
            }
        }
        public frmQLNhapHang(string strCon)
        {
            InitializeComponent();
            this.strCon = strCon;
            LoadNhapHang();
        }

        private void frmQLNhapHang_Load(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int maDonNhap;
            if (!int.TryParse(txtTimKiem.Text.Trim(), out maDonNhap))
            {
                MessageBox.Show("Vui lòng nhập mã đơn nhập hợp lệ!");
                return;
            }

            using (SqlConnection con = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM dbo.fn_TimKiemDonNhapHangTheoMa(@MaDonNhap)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaDonNhap", maDonNhap);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQLNhapHang.DataSource = dt;
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            LoadNhapHang();
            MessageBox.Show("Làm mới dữ liệu nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTinhToan_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

            decimal tongTien = 0;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string query = "SELECT dbo.fn_TongTienNhapHangTheoNgay(@NgayBatDau, @NgayKetThuc)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                    tongTien = Convert.ToDecimal(result);
                con.Close();
            }

            MessageBox.Show(
                $"Tổng tiền nhập hàng từ ngày {ngayBatDau:dd/MM/yyyy} đến ngày {ngayKetThuc:dd/MM/yyyy} là {tongTien:N0} VNĐ",
                "Kết quả",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

            if (ngayBatDau > ngayKetThuc)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Cảnh báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM fn_TraCuuPhieuNhapHangTheoNgay(@NgayBatDau, @NgayKetThuc)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvQLNhapHang.DataSource = dt;
                }
            }
        }
    }
}
