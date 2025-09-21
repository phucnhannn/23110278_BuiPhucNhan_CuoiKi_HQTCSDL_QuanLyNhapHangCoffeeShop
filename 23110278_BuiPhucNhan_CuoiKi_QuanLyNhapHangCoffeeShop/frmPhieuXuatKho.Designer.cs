namespace _23110278_BuiPhucNhan_CuoiKi_QuanLyNhapHangCoffeeShop
{
    partial class frmPhieuXuatKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.dgvXuatKho = new System.Windows.Forms.DataGridView();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtLyDoXuatKho = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNguyenLieu = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(907, 479);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 84);
            this.label7.TabIndex = 31;
            this.label7.Text = "(xóa khi thêm nhầm 1 dòng vào bảng)";
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnXoa.Location = new System.Drawing.Point(910, 429);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 47);
            this.btnXoa.TabIndex = 30;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(419, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 28);
            this.label6.TabIndex = 29;
            this.label6.Text = "Bảng xuất kho";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnXacNhan.Location = new System.Drawing.Point(392, 652);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(203, 54);
            this.btnXacNhan.TabIndex = 28;
            this.btnXacNhan.Text = "Xác nhận xuất kho";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click_1);
            // 
            // dgvXuatKho
            // 
            this.dgvXuatKho.AllowDrop = true;
            this.dgvXuatKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvXuatKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXuatKho.Location = new System.Drawing.Point(87, 290);
            this.dgvXuatKho.Name = "dgvXuatKho";
            this.dgvXuatKho.ReadOnly = true;
            this.dgvXuatKho.RowHeadersWidth = 51;
            this.dgvXuatKho.RowTemplate.Height = 24;
            this.dgvXuatKho.Size = new System.Drawing.Size(808, 356);
            this.dgvXuatKho.TabIndex = 27;
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnThem.Location = new System.Drawing.Point(703, 149);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(119, 67);
            this.btnThem.TabIndex = 26;
            this.btnThem.Text = "Thêm vào bảng xuất";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtLyDoXuatKho
            // 
            this.txtLyDoXuatKho.Location = new System.Drawing.Point(515, 98);
            this.txtLyDoXuatKho.Name = "txtLyDoXuatKho";
            this.txtLyDoXuatKho.Size = new System.Drawing.Size(325, 22);
            this.txtLyDoXuatKho.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F);
            this.label5.Location = new System.Drawing.Point(133, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(356, 24);
            this.label5.TabIndex = 24;
            this.label5.Text = "Lý do xuất kho (chung cho cả phiếu xuất):";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(407, 190);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(275, 22);
            this.txtSoLuong.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F);
            this.label4.Location = new System.Drawing.Point(160, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "Nhập số lượng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 35);
            this.label2.TabIndex = 19;
            this.label2.Text = "Phiếu xuất kho";
            // 
            // cbNguyenLieu
            // 
            this.cbNguyenLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNguyenLieu.FormattingEnabled = true;
            this.cbNguyenLieu.Location = new System.Drawing.Point(407, 152);
            this.cbNguyenLieu.Name = "cbNguyenLieu";
            this.cbNguyenLieu.Size = new System.Drawing.Size(275, 24);
            this.cbNguyenLieu.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(160, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Chọn nguyên liệu:";
            // 
            // frmPhieuXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(1001, 718);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.dgvXuatKho);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtLyDoXuatKho);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbNguyenLieu);
            this.Controls.Add(this.label1);
            this.Name = "frmPhieuXuatKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu xuất kho";
            this.Load += new System.EventHandler(this.frmPhieuXuatKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.DataGridView dgvXuatKho;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtLyDoXuatKho;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbNguyenLieu;
        private System.Windows.Forms.Label label1;
    }
}