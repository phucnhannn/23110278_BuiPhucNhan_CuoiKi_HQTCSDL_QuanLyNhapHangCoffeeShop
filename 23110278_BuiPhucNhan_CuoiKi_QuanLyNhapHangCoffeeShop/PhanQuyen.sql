CREATE LOGIN nvnh WITH PASSWORD = '1';
CREATE LOGIN nvk WITH PASSWORD = '2';

CREATE USER nvnh FOR LOGIN nvnh;
CREATE USER nvk FOR LOGIN nvk;

CREATE ROLE role_nvnh;  
CREATE ROLE role_nvk;

GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.PurchaseOrder TO role_nvnh;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.PurchaseOrderDetail TO role_nvnh;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Supplier TO role_nvnh;
GRANT SELECT ON dbo.view_QuanLyNhapHang TO role_nvnh;
GRANT SELECT ON dbo.Material TO role_nvnh;

GRANT EXECUTE ON dbo.sp_ShowNguyenLieu TO role_nvnh;
GRANT EXECUTE ON dbo.sp_ShowNhaCungCap TO role_nvnh;
GRANT EXECUTE ON dbo.sp_SuaNhaCungCap TO role_nvnh;
GRANT EXECUTE ON dbo.sp_ThemChiTietPhieuNhapHang TO role_nvnh;
GRANT EXECUTE ON dbo.sp_ThemNhaCungCap TO role_nvnh;
GRANT EXECUTE ON dbo.sp_ThemPhieuNhapHang TO role_nvnh;
GRANT EXECUTE ON dbo.sp_XoaNhaCungCap TO role_nvnh;

GRANT SELECT ON dbo.fn_TimKiemDonNhapHangTheoMa TO role_nvnh;
GRANT SELECT ON dbo.fn_TimKiemNhaCungCap TO role_nvnh;
GRANT SELECT ON dbo.fn_TimKiemNguyenLieu TO role_nvnh;
GRANT SELECT ON dbo.fn_TimKiemKhoTheoTenNguyenLieu TO role_nvnh;
GRANT SELECT ON dbo.fn_TraCuuPhieuNhapHangTheoNgay TO role_nvnh;

GRANT EXECUTE ON dbo.fn_KiemTraTonKho TO role_nvnh;
GRANT EXECUTE ON dbo.fn_TongTienNhapHangTheoNgay TO role_nvnh;

GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.ExportVoucher TO role_nvk;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.ExportVoucherDetail TO role_nvk;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Material TO role_nvk;
GRANT SELECT ON dbo.InventoryLot TO role_nvk;
GRANT SELECT ON dbo.view_ThongTinKho TO role_nvk;
GRANT SELECT ON dbo.view_XuatKho_ChiTiet TO role_nvk;

GRANT EXECUTE ON dbo.sp_ShowNguyenLieu TO role_nvk;
GRANT EXECUTE ON dbo.sp_SuaNguyenLieu TO role_nvk;
GRANT EXECUTE ON dbo.sp_ThemChiTietPhieuXuatKho TO role_nvk;
GRANT EXECUTE ON dbo.sp_ThemNguyenLieu TO role_nvk;
GRANT EXECUTE ON dbo.sp_ThemPhieuXuatKho TO role_nvk;
GRANT EXECUTE ON dbo.sp_XoaNguyenLieu TO role_nvk;

GRANT SELECT ON dbo.fn_TimKiemNguyenLieu TO role_nvk;
GRANT SELECT ON dbo.fn_TimKiemKhoTheoTenNguyenLieu TO role_nvk;
GRANT SELECT ON dbo.fn_TraCuuNguyenLieuSapHetHan TO role_nvk;
GRANT SELECT ON dbo.fn_TraCuuPhieuXuatKhoTheoNgay TO role_nvk;
GRANT SELECT ON dbo.fn_TimKiemPhieuXuatKhoTheoMa TO role_nvk;

GRANT EXECUTE ON dbo.fn_KiemTraTonKho TO role_nvk;
GRANT EXECUTE ON dbo.fn_TongTienNhapHangTheoNgay TO role_nvk;

ALTER ROLE role_nvnh ADD MEMBER nvnh;
ALTER ROLE role_nvk ADD MEMBER nvk;