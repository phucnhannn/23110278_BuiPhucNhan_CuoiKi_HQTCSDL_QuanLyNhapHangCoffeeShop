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
    public partial class frmQLXuatKho : Form
    {
        string strCon;
        public frmQLXuatKho(string strCon)
        {
            InitializeComponent();
            this.strCon = strCon;
        }

        private void frmQLXuatKho_Load(object sender, EventArgs e)
        {
            LoadXuatKho();
        }
        private void LoadXuatKho()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM view_XuatKho_ChiTiet";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQLXuatKho.DataSource = dt;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadXuatKho();
            MessageBox.Show("Làm mới dữ liệu quản lý xuất kho thành công.", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text) || !int.TryParse(txtTimKiem.Text, out int maXuatKho))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu xuất kho hợp lệ.");
                return;
            }
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM fn_TimKiemPhieuXuatKhoTheoMa(@MaXuatKho)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaXuatKho", maXuatKho);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvQLXuatKho.DataSource = dt;
                }
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

            if (ngayBatDau > ngayKetThuc)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM fn_TraCuuPhieuXuatKhoTheoNgay(@NgayBatDau, @NgayKetThuc)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvQLXuatKho.DataSource = dt;
                }
            }
        }
    }
}
