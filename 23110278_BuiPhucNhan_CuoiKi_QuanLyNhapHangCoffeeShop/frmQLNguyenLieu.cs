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
    public partial class frmQLNguyenLieu : Form
    {
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        private int materialId = -1;
        public frmQLNguyenLieu()
        {
            InitializeComponent();
        }
        public frmQLNguyenLieu(int id, string name, string unit, int reorderLevel)
        {
            InitializeComponent();
            materialId = id;
            txtTenNguyenLieu.Text = name;
            txtDonViTinh.Text = unit;
            txtLuongTonToiThieu.Text = reorderLevel.ToString();
        }

        private void frmQLNguyenLieu_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd;
                if (materialId == -1)
                {
                    cmd = new SqlCommand("sp_ThemNguyenLieu", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaterialName", txtTenNguyenLieu.Text.Trim());
                    cmd.Parameters.AddWithValue("@Unit", string.IsNullOrWhiteSpace(txtDonViTinh.Text) ? (object)DBNull.Value : txtDonViTinh.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReorderLevel", string.IsNullOrWhiteSpace(txtLuongTonToiThieu.Text) ? (object)DBNull.Value : Convert.ToInt32(txtLuongTonToiThieu.Text));
                }
                else
                {
                    cmd = new SqlCommand("sp_SuaNguyenLieu", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaterialID", materialId);
                    cmd.Parameters.AddWithValue("@MaterialName", txtTenNguyenLieu.Text.Trim());
                    cmd.Parameters.AddWithValue("@Unit", string.IsNullOrWhiteSpace(txtDonViTinh.Text) ? (object)DBNull.Value : txtDonViTinh.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReorderLevel", string.IsNullOrWhiteSpace(txtLuongTonToiThieu.Text) ? (object)DBNull.Value : Convert.ToInt32(txtLuongTonToiThieu.Text));
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Lưu thông tin nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
