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
    public partial class frmQLKho : Form
    {
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmQLKho()
        {
            InitializeComponent();
        }

        private void frmQLKho_Load(object sender, EventArgs e)
        {
            LoadKho();
        }
        private void LoadKho()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM view_ThongTinKho";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQLKho.DataSource = dt;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadKho();
            MessageBox.Show("Làm mới dữ liệu trong kho thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenNguyenLieu = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tenNguyenLieu))
            {
                MessageBox.Show("Vui lòng nhập tên nguyên liệu cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM fn_TimKiemKhoTheoTenNguyenLieu(@TenNguyenLieu)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvQLKho.DataSource = dt;
                }
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT * FROM fn_TraCuuNguyenLieuSapHetHan()";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQLKho.DataSource = dt;
            }
        }
    }
}
