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
    public partial class frmPhieuXuatKho : Form
    {
        string strCon = @"Data Source=JOHNNYBUIII;Initial Catalog=QuanLyNhapHang;User ID=sa;Password=1;TrustServerCertificate=True";
        public frmPhieuXuatKho()
        {
            InitializeComponent();
            LoadNguyenLieu();
        }

        private void frmPhieuXuatKho_Load(object sender, EventArgs e)
        {
            if (dgvXuatKho.Columns.Count == 0)
            {
                dgvXuatKho.Columns.Add("TenNguyenLieu", "Tên nguyên liệu");
                dgvXuatKho.Columns.Add("SoLuong", "Số lượng");
            }
        }
        private void LoadNguyenLieu()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT MaterialID, MaterialName FROM Material";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNguyenLieu.DataSource = dt;
                cbNguyenLieu.DisplayMember = "MaterialName";
                cbNguyenLieu.ValueMember = "MaterialID";
                cbNguyenLieu.SelectedIndex = -1;
            }
        }

        private int GetMaterialIDByName(string materialName)
        {
            DataTable dt = cbNguyenLieu.DataSource as DataTable;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaterialName"].ToString() == materialName)
                        return Convert.ToInt32(row["MaterialID"]);
                }
            }
            return -1;
        }

        private int GetFirstAvailableLotID(int materialID, int quantity, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 LotID FROM InventoryLot WHERE MaterialID = @MaterialID AND RemainingQuantity >= @Quantity ORDER BY ExpiryDate", conn))
            {
                cmd.Parameters.AddWithValue("@MaterialID", materialID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbNguyenLieu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLyDoXuatKho.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do xuất kho.");
                return;
            }

            string tenNguyenLieu = cbNguyenLieu.Text;
            string lyDo = txtLyDoXuatKho.Text;

            dgvXuatKho.Rows.Add(tenNguyenLieu, soLuong);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvXuatKho.SelectedRows.Count > 0)
            {
                dgvXuatKho.Rows.RemoveAt(dgvXuatKho.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (dgvXuatKho.Rows.Count <= 1)
            {
                MessageBox.Show("Chưa có nguyên liệu nào trong bảng xuất kho.");
                return;
            }

            string lyDo = txtLyDoXuatKho.Text;
            DateTime exportDate = DateTime.Now;
            int exportVoucherID;

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                // 1. Thêm phiếu xuất kho
                using (SqlCommand cmd = new SqlCommand("sp_ThemPhieuXuatKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExportDate", exportDate);
                    cmd.Parameters.AddWithValue("@Reason", lyDo);
                    exportVoucherID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Thêm từng chi tiết phiếu xuất kho
                foreach (DataGridViewRow row in dgvXuatKho.Rows)
                {
                    if (row.IsNewRow) continue;

                    string materialName = row.Cells[0].Value.ToString();
                    int materialID = GetMaterialIDByName(materialName);
                    int quantity = Convert.ToInt32(row.Cells[1].Value);

                    // Lấy LotID còn tồn kho (ví dụ lấy lô đầu tiên còn hàng)
                    int lotID = GetFirstAvailableLotID(materialID, quantity, conn);
                    if (lotID == -1)
                    {
                        MessageBox.Show($"Không đủ tồn kho cho nguyên liệu {materialName}");
                        continue;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_ThemChiTietPhieuXuatKho", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ExportVoucherID", exportVoucherID);
                        cmd.Parameters.AddWithValue("@MaterialID", materialID);
                        cmd.Parameters.AddWithValue("@LotID", lotID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }

            MessageBox.Show("Xuất kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvXuatKho.Rows.Clear();
        }
    }
}
