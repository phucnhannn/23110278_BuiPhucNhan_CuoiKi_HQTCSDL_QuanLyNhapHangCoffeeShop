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
    public partial class frmPhieuNhapHang : Form
    {
        string strCon;
        public frmPhieuNhapHang(string strCon)
        {
            this.strCon = strCon;
            InitializeComponent();
            LoadNhaCungCap();
            LoadNguyenLieu();
        }
        private void LoadNhaCungCap()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string query = "SELECT SupplierID, SupplierName FROM Supplier";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNhaCungCap.DataSource = dt;
                cbNhaCungCap.DisplayMember = "SupplierName";
                cbNhaCungCap.ValueMember = "SupplierID";
                cbNhaCungCap.SelectedIndex = -1; 
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
        private void frmPhieuNhapHang_Load(object sender, EventArgs e)
        {
            if (dgvNhapHang.Columns.Count == 0)
            {
                dgvNhapHang.Columns.Add("MaterialName", "Tên nguyên liệu");
                dgvNhapHang.Columns.Add("Quantity", "Số lượng");
                dgvNhapHang.Columns.Add("UnitPrice", "Giá (trên 1 đơn vị)");
                dgvNhapHang.Columns.Add("ExpiryDate", "Hạn sử dụng");
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
            if (string.IsNullOrWhiteSpace(txtGia.Text) || !decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là số dương.");
                return;
            }
            string tenNguyenLieu = cbNguyenLieu.Text;
            DateTime expiryDate = dtpHanSuDung.Value.Date;
            dgvNhapHang.Rows.Add(tenNguyenLieu, soLuong, gia, expiryDate.ToString("yyyy-MM-dd"));
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhapHang.SelectedRows.Count > 0)
            {
                dgvNhapHang.Rows.RemoveAt(dgvNhapHang.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (cbNhaCungCap.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.");
                return;
            }
            if (dgvNhapHang.Rows.Count <= 1)
            {
                MessageBox.Show("Chưa có nguyên liệu nào trong bảng nhập hàng.");
                return;
            }

            int supplierID = Convert.ToInt32(cbNhaCungCap.SelectedValue);
            DateTime orderDate = DateTime.Now;
            decimal totalPrice = 0;

            foreach (DataGridViewRow row in dgvNhapHang.Rows)
            {
                if (row.IsNewRow) continue;
                totalPrice += Convert.ToDecimal(row.Cells["Quantity"].Value) * Convert.ToDecimal(row.Cells["UnitPrice"].Value);
            }

            int purchaseOrderID = -1;
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                // 1. Thêm phiếu nhập hàng, lấy PurchaseOrderID mới
                using (SqlCommand cmd = new SqlCommand("sp_ThemPhieuNhapHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                    purchaseOrderID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Thêm từng chi tiết phiếu nhập hàng
                foreach (DataGridViewRow row in dgvNhapHang.Rows)
                {
                    if (row.IsNewRow) continue;

                    string materialName = row.Cells["MaterialName"].Value.ToString();
                    int materialID = GetMaterialIDByName(materialName);
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    DateTime expiryDate = DateTime.Parse(row.Cells["ExpiryDate"].Value.ToString());

                    using (SqlCommand cmd = new SqlCommand("sp_ThemChiTietPhieuNhapHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                        cmd.Parameters.AddWithValue("@MaterialID", materialID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }

            MessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvNhapHang.Rows.Clear();
        }
    }
}
