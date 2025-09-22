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
        string strCon;
        public frmPhieuXuatKho(string strCon)
        {
            this.strCon = strCon;
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

        private bool CheckTonKho(int materialID, int soLuongCanXuat, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.fn_KiemTraTonKho(@MaterialID, @SoLuongCanXuat)", conn))
            {
                cmd.Parameters.AddWithValue("@MaterialID", materialID);
                cmd.Parameters.AddWithValue("@SoLuongCanXuat", soLuongCanXuat);
                object result = cmd.ExecuteScalar();
                return Convert.ToBoolean(result);
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

        private void btnXacNhan_Click_1(object sender, EventArgs e)
        {
            if (dgvXuatKho.Rows.Count <= 1)
            {
                MessageBox.Show("Chưa có nguyên liệu nào trong bảng xuất kho.");
                return;
            }

            string lyDo = txtLyDoXuatKho.Text;
            DateTime exportDate = DateTime.Now;
            int exportVoucherID;
            bool xuatThanhCong = false;
            string canhBao = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dgvXuatKho.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string materialName = row.Cells[0].Value.ToString();
                        int materialID = GetMaterialIDByName(materialName);
                        int quantity = Convert.ToInt32(row.Cells[1].Value);

                        if (!CheckTonKho(materialID, quantity, conn))
                        {
                            MessageBox.Show($"Không đủ tồn kho cho nguyên liệu {materialName}. Cần {quantity}");
                            conn.Close();
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_ThemPhieuXuatKho", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ExportDate", exportDate);
                        cmd.Parameters.AddWithValue("@Reason", lyDo);
                        exportVoucherID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    foreach (DataGridViewRow row in dgvXuatKho.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string materialName = row.Cells[0].Value.ToString();
                        int materialID = GetMaterialIDByName(materialName);
                        int quantity = Convert.ToInt32(row.Cells[1].Value);

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_ThemChiTietPhieuXuatKho", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ExportVoucherID", exportVoucherID);
                                cmd.Parameters.AddWithValue("@MaterialID", materialID);
                                cmd.Parameters.AddWithValue("@LotID", 0);
                                cmd.Parameters.AddWithValue("@Quantity", quantity);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException ex_inner)
                        {
                            if (ex_inner.Number == 50000) 
                            {
                                canhBao = ex_inner.Message;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    xuatThanhCong = true;
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (xuatThanhCong)
            {
                MessageBox.Show("Xuất kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!string.IsNullOrEmpty(canhBao))
                {
                    MessageBox.Show(canhBao, "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dgvXuatKho.Rows.Clear();
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
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
    }
}
