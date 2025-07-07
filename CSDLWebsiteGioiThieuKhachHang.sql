-- Sử dụng master để tạo CSDL
USE master;
GO

-- Tạo cơ sở dữ liệu
CREATE DATABASE WebsiteGioiThieuNhaHang;
GO

-- Dùng CSDL vừa tạo
USE WebsiteGioiThieuNhaHang;
GO

-- 1. Loại món ăn
CREATE TABLE LoaiMon (
    IdLoai INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

-- 2. Món ăn
CREATE TABLE MonAn (
    IdMonAn INT PRIMARY KEY IDENTITY(1,1),
    TenMon NVARCHAR(150) NOT NULL,
    MoTa TEXT,
    Gia DECIMAL(10,2) NOT NULL,
    HinhAnh NVARCHAR(255),
    IdLoai INT,
    FOREIGN KEY (IdLoai) REFERENCES LoaiMon(IdLoai)
);

-- 3. Đặt bàn
CREATE TABLE DatBan (
    IdDatBan INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(20) NOT NULL,
    Email VARCHAR(100),
    ThoiGian DATETIME NOT NULL,
    SoNguoi INT NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT 'Chờ xác nhận'
);

-- 4. Tin tức
CREATE TABLE TinTuc (
    IdTinTuc INT PRIMARY KEY IDENTITY(1,1),
    TieuDe NVARCHAR(200) NOT NULL,
    NoiDung TEXT,
    HinhAnh NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE()
);

-- 5. Liên hệ
CREATE TABLE LienHe (
    IdLienHe INT PRIMARY KEY IDENTITY(1,1),
    HoTen VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    TinNhan TEXT,
    NgayTao DATETIME DEFAULT GETDATE()
);

-- 6. Tài khoản quản trị
CREATE TABLE AdminUsers (
    IdAD INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
	Password VARCHAR(255) NOT NULL,
    --PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) DEFAULT 'staff'
);

-- 7. Nhật ký thao tác của admin
CREATE TABLE AdminLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    IdAD INT,
    HanhDong TEXT,
    ThoiGian DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdAD) REFERENCES AdminUsers(IdAD)
);

-- 8. Vai trò
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(50) NOT NULL
);

-- 9. Gán vai trò cho admin
CREATE TABLE AdminRoles (
    IdAD INT,
    RoleID INT,
    PRIMARY KEY (IdAD, RoleID),
    FOREIGN KEY (IdAD) REFERENCES AdminUsers(IdAD),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- 10. Khách hàng
CREATE TABLE KhachHang (
    IdKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    MatKhau NVARCHAR(100)
);
ALTER TABLE DatBan
ADD IdKH INT;
-- khóa ngoại
ALTER TABLE DatBan
ADD CONSTRAINT FK_DatBan_KhachHang
FOREIGN KEY (IdKH) REFERENCES KhachHang(IdKH);



-- Email sẽ là username để đăng nhập.
--MatKhau lưu tạm dạng plain text cho demo, thực tế nên mã hóa
DROP TABLE KhachHang;


-- =============================
-- DỮ LIỆU MẪU
-- =============================

-- Thêm loại món ăn
INSERT INTO LoaiMon (Name) VALUES
('Món chính'),
('Tráng miệng'),
('Đồ uống');

-- Thêm món ăn
INSERT INTO MonAn (TenMon, MoTa, Gia, HinhAnh, IdLoai) VALUES
(N'Phở bò', N'Phở bò với nước dùng đậm đà nấu trong 12 giờ, thịt bò tươi ngon cùng các loại gia vị đặc trưng.', 120.000, N'wwwroot/AnhnhaHang/nhaHangMenuPhoBo.jpeg', 1),
(N'Bánh flan', N'Món tráng miệng ngọt', 20000, N'wwwroot/AnhnhaHang/nhaHangMenuBanhFlan.png', 2),
(N'Trà đào', N'Đồ uống giải khát', 30000, 'wwwroot/AnhnhaHang/nhaHangMenuTraDao.png', 3),
(N'Bún chả', N'Bún chả với thịt nướng thơm lừng, nước mắm chua ngọt đặc trưng và rau sống tươi ngon.', 95000, N'wwwroot/AnhnhaHang/nhaHangMenuBunCha.jpg', 1),
(N'Gỏi cuốn tôm thịt', N'Gỏi cuốn tươi mát với tôm, thịt heo, bún, rau thơm cuộn trong bánh tráng, kèm nước chấm.', 85000, N'wwwroot/AnhnhaHang/nhaHangMenuGoiCuon.png', 1),
(N'Bánh Xèo', N'Thưởng thức hương vị đậm chất miền Trung với bánh xèo nhân tôm thịt đặc biệt.', 100000, N'wwwroot/AnhnhaHang/nhaHangMenuBanhXeo.jpeg', 1);

-- Thêm tin tức
INSERT INTO TinTuc (TieuDe, NoiDung, HinhAnh) VALUES
('Ra mắt món mới: Bánh xèo miền Trung', 'Thưởng thức hương vị đậm chất miền Trung với bánh xèo nhân tôm thịt đặc biệt.', 'wwwroot/AnhnhaHang/nhaHangMenuBanhXeo.jpeg'),
('Ưu đãi tháng 6: Giảm 20% cho đơn trên 500k', 'Chương trình khuyến mãi hấp dẫn đang chờ đón bạn từ ngày 1 đến 30 tháng 6!', 'wwwroot/AnhnhaHang/nhaHangTinTucKhuyenMai1.jpg'),
('Ưu đãi Siêu Hời: Giảm 10% khi thanh toán bằng các Thẻ Ngân Hàng VIB - TECHCOMBANK - VPBANK - TPBANK', 'khách hàng được giảm giá khi dùng thanh toán bằng thẻ ngân hàng liên kết với nhà hàng.','wwwroot/AnhnhaHang/nhaHangTinTucKhuyenMai3.jpg');

-- Thêm đặt bàn
--INSERT INTO DatBan (HoTen, SDT, Email, ThoiGian, SoNguoi) VALUES
--('Nguyễn Văn A', '0909123456', 'a@gmail.com', '2025-06-30 19:00:00', 4);

INSERT INTO DatBan (HoTen, SDT, Email, ThoiGian, SoNguoi, DaXacNhan, TrangThai) VALUES
(N'Nguyễn Văn A', '0909123456', 'a@gmail.com', '2025-07-02 18:00:00', 4, 0, N'Chờ xác nhận');
-- điền 1 thay 0 để đổi thành đã xác nhận 




-- Thêm liên hệ
INSERT INTO LienHe (HoTen, Email, TinNhan) VALUES
('Trần Thị B', 'b@gmail.com', 'Nhà hàng có phục vụ chay không?');

-- Thêm tài khoản admin (mật khẩu: 123456 - plain text để demo, nên dùng mã hóa trong thực tế)
--INSERT INTO AdminUsers (Username, PasswordHash, Role)
--VALUES ('admin', '123456', 'admin');

INSERT INTO AdminUsers (Username, Password, Role)
VALUES ('admin', '123456', 'admin');

UPDATE AdminUsers
SET Password = '123456', Role = 'admin'
WHERE Username = 'admin';

--gán tất cả quyền cho id
-- Tránh lỗi nếu đã tồn tại
IF NOT EXISTS (
    SELECT * FROM AdminRoles WHERE IdAD = 1 AND RoleID = 4
)
BEGIN
    INSERT INTO AdminRoles (IdAD, RoleID)
    VALUES (1, 4);
END

INSERT INTO AdminRoles (IdAD, RoleID)
SELECT 1, r.RoleID
FROM Roles r
WHERE NOT EXISTS (
    SELECT 1 FROM AdminRoles ar WHERE ar.IdAD = 1 AND ar.RoleID = r.RoleID
);

USE WebsiteGioiThieuNhaHang;
GO

EXEC sp_rename 'AdminUsers.PasswordHash', 'Password', 'COLUMN';
EXEC sp_rename 'dbo.AdminUsers.PasswordHash', 'Password', 'COLUMN';
SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'AdminUsers';
SELECT * FROM AdminUsers;


SELECT * FROM AdminRoles WHERE IdAD = 1;

SELECT IdAD, Username FROM AdminUsers;

-- Thêm vai trò
INSERT INTO Roles (RoleName) VALUES
('Quản lý món ăn'),
('Quản lý đặt bàn'),
('Quản lý tin tức');

-- Gán quyền cho admin (IdAD = 1)
INSERT INTO AdminRoles (IdAD, RoleID) VALUES
(1, 1), (1, 2), (1, 3);
-- Thêm quyền "Quản lý tin tức" nếu chưa có (giả sử RoleID = 3)
INSERT INTO AdminRoles (IdAD, RoleID) VALUES (1, 3);


-- Ghi log thao tác admin
INSERT INTO AdminLogs (IdAD, HanhDong) VALUES
(1, 'Đã thêm món Phở bò');

-- =============================
-- KIỂM TRA DỮ LIỆU
-- =============================
--ktra vai tro
SELECT * FROM AdminRoles WHERE IdAD = 1;


-- Kiểm tra tài khoản admin
SELECT * FROM AdminUsers;

-- Kiểm tra quyền
SELECT au.Username, r.RoleName
FROM AdminUsers au
JOIN AdminRoles ar ON au.IdAD = ar.IdAD
JOIN Roles r ON ar.RoleID = r.RoleID;

-- Kiểm tra món ăn
SELECT m.TenMon, l.Name AS LoaiMon, m.Gia
FROM MonAn m
JOIN LoaiMon l ON m.IdLoai = l.IdLoai;

-- Kiểm tra đặt bàn
SELECT * FROM DatBan;

-- Kiểm tra liên hệ
SELECT * FROM LienHe;

-- Đổi tên cột từ PasswordHash → Password
--EXEC sp_rename 'AdminUsers.PasswordHash', 'Password', 'COLUMN';


SELECT * FROM LoaiMon;
SELECT TenMon, IdLoai FROM MonAn;
SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'DatBan';
ALTER TABLE DatBan ADD DaXacNhan BIT DEFAULT 0;
SELECT * FROM DatBan WHERE ThoiGian IS NULL;
UPDATE DatBan SET ThoiGian = GETDATE() WHERE ThoiGian IS NULL;
UPDATE DatBan SET ThoiGian = GETDATE() WHERE ThoiGian IS NULL;

UPDATE DatBan
SET DaXacNhan = 0
WHERE DaXacNhan IS NULL;



UPDATE MonAn SET TenMon = N'Phở bò' WHERE TenMon LIKE '%Ph?%';
UPDATE MonAn SET TenMon = N'Bún chả' WHERE TenMon LIKE '%Bún ch?%';
UPDATE MonAn SET TenMon = N'Bánh flan' WHERE TenMon LIKE '%Bánh flan%';
UPDATE MonAn SET TenMon = N'Trà đào' WHERE TenMon LIKE '%Trà đào%';
UPDATE MonAn SET TenMon = N'Gỏi cuốn tôm thịt' WHERE TenMon LIKE '%G?i%';

UPDATE MonAn
SET MoTa = N'Phở bò với nước dùng đậm đà nấu trong 12 giờ, thịt bò tươi ngon cùng các loại gia vị đặc trưng.'
WHERE TenMon = N'Phở bò';

UPDATE MonAn
SET MoTa = N'Bún chả với thịt nướng thơm lừng, nước mắm chua ngọt đặc trưng và rau sống tươi ngon.'
WHERE TenMon = N'Bún chả';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuPhoBo.jpeg'
WHERE TenMon = N'Phở bò';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuBanhFlan.png'
WHERE TenMon = N'Bánh flan';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuTraDao.png'
WHERE TenMon = N'Trà đào';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuBunCha.jpg'
WHERE TenMon = N'Bún chả';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuGoiCuon.png'
WHERE TenMon = N'Gỏi cuốn tôm thịt';

UPDATE MonAn
SET HinhAnh = '/AnhnhaHang/nhaHangMenuBanhXeo.jpeg'
WHERE TenMon = N'Bánh Xèo';

UPDATE LoaiMon SET Name = N'Món chính' WHERE Name LIKE '%ch?nh%';
UPDATE LoaiMon SET Name = N'Tráng miệng' WHERE Name LIKE '%mi?ng%';
UPDATE LoaiMon SET Name = N'Đồ uống' WHERE Name LIKE '%u?ng%';

SELECT TenMon, MoTa FROM MonAn

--xóa hết TT món ăn rồi chèn lại
-- Xóa toàn bộ dữ liệu trong bảng MonAn
DELETE FROM MonAn;

-- Chèn lại đúng 5 món ăn
INSERT INTO MonAn (TenMon, MoTa, Gia, HinhAnh, IdLoai) VALUES
(N'Phở bò', N'Phở bò với nước dùng đậm đà nấu trong 12 giờ, thịt bò tươi ngon cùng các loại gia vị đặc trưng.', 120000, N'/AnhnhaHang/nhaHangMenuPhoBo.jpeg', 1),
(N'Bánh flan', N'Món tráng miệng ngọt', 20000, N'/AnhnhaHang/nhaHangMenuBanhFlan.png', 2),
(N'Trà đào', N'Đồ uống giải khát', 30000, N'/AnhnhaHang/nhaHangMenuTraDao.png', 3),
(N'Bún chả', N'Bún chả với thịt nướng thơm lừng, nước mắm chua ngọt đặc trưng và rau sống tươi ngon.', 95000, N'/AnhnhaHang/nhaHangMenuBunCha.jpg', 1),
(N'Gỏi cuốn tôm thịt', N'Gỏi cuốn tươi mát với tôm, thịt heo, bún, rau thơm cuộn trong bánh tráng, kèm nước chấm.', 85000, N'/AnhnhaHang/nhaHangMenuGoiCuon.png', 1);
SELECT COUNT(*) AS SoLuong, TenMon
FROM MonAn
GROUP BY TenMon
HAVING COUNT(*) > 1;

SELECT CAST(TenMon AS VARBINARY) FROM MonAn;

--adminusser
SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'AdminUsers';
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AdminUsers';

INSERT INTO AdminUsers (Username, Password, Role)
VALUES ('admin', '123456', 'admin');


-------------------------------------- mẫu thêm dữ liệu
-- Thêm đặt bàn
INSERT INTO DatBan (HoTen, SDT, Email, ThoiGian, SoNguoi, DaXacNhan, TrangThai) VALUES
(N'Nguyễn Văn A', '0909123456', 'a@gmail.com', '2025-07-02 18:00:00', 4, 0, N'Chờ xác nhận');

SET IDENTITY_INSERT LoaiMon ON;
INSERT INTO LoaiMon (IdLoai, Name) VALUES
(1, N'Món chính'),
(2, N'Tráng miệng'),
(3, N'Đồ uống');
SET IDENTITY_INSERT LoaiMon OFF;
-- Thêm món ăn
INSERT INTO MonAn (TenMon, MoTa, Gia, HinhAnh, IdLoai) VALUES
(N'Phở bò', N'Phở bò với nước dùng đậm đà nấu trong 12 giờ, thịt bò tươi ngon cùng các loại gia vị đặc trưng.', 120.000, N'/AnhnhaHang/nhaHangMenuPhoBo.jpeg', 1),
(N'Bánh flan', N'Món tráng miệng ngọt', 20000, N'/AnhnhaHang/nhaHangMenuBanhFlan.png', 2),
(N'Trà đào', N'Đồ uống giải khát', 30000, '/AnhnhaHang/nhaHangMenuTraDao.png', 3),
(N'Bún chả', N'Bún chả với thịt nướng thơm lừng, nước mắm chua ngọt đặc trưng và rau sống tươi ngon.', 95000, N'/AnhnhaHang/nhaHangMenuBunCha.jpg', 1),
(N'Gỏi cuốn tôm thịt', N'Gỏi cuốn tươi mát với tôm, thịt heo, bún, rau thơm cuộn trong bánh tráng, kèm nước chấm.', 85000, N'/AnhnhaHang/nhaHangMenuGoiCuon.png', 1),
(N'Bánh Xèo', N'Thưởng thức hương vị đậm chất miền Trung với bánh xèo nhân tôm thịt đặc biệt.', 100000, N'/AnhnhaHang/nhaHangMenuBanhXeo.jpeg', 1);
DELETE FROM MonAn;

SELECT * FROM KhachHang;