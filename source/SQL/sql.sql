USE [StoreShoes]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonBan]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonBan](
	[MaHDB] [nvarchar](255) NOT NULL,
	[MaSPG] [nvarchar](30) NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_ChiTietHoaDonBan_SoLuong]  DEFAULT ((1)),
 CONSTRAINT [PK_ChiTietHoaDonBan] PRIMARY KEY CLUSTERED 
(
	[MaHDB] ASC,
	[MaSPG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietHoaDonMua]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonMua](
	[MaHDM] [nvarchar](30) NOT NULL,
	[MaSPG] [nvarchar](30) NOT NULL,
	[SoLuong] [int] NULL CONSTRAINT [DF_ChiTietHoaDonMua_SoLuong]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHoaDonMua] PRIMARY KEY CLUSTERED 
(
	[MaHDM] ASC,
	[MaSPG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoaDonBan]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonBan](
	[MaHDB] [nvarchar](255) NOT NULL,
	[MaNV] [nvarchar](30) NULL,
	[NgayBan] [date] NULL CONSTRAINT [DF_HoaDonBan_NgayBan]  DEFAULT (getdate()),
	[TongTien] [nvarchar](53) NULL,
 CONSTRAINT [PK__HoaDonBa__3C90E8FA2676CD74] PRIMARY KEY CLUSTERED 
(
	[MaHDB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoaDonMua]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonMua](
	[MaHDM] [nvarchar](30) NOT NULL,
	[MaNV] [nvarchar](30) NULL,
	[NgayMua] [date] NULL CONSTRAINT [DF_HoaDonMua_NgayMua]  DEFAULT (getdate()),
	[TongTien] [nvarchar](50) NULL,
 CONSTRAINT [PK__HoaDonMu__3C90E8C5991E064E] PRIMARY KEY CLUSTERED 
(
	[MaHDM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiGiay]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiGiay](
	[MaLG] [nvarchar](30) NOT NULL,
	[Ten] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__LoaiGiay__2725C77EC60AB385] PRIMARY KEY CLUSTERED 
(
	[MaLG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [nvarchar](30) NOT NULL,
	[TenNCC] [nvarchar](255) NOT NULL,
	[DiaChi] [nvarchar](255) NOT NULL,
	[SDT] [nvarchar](255) NULL,
 CONSTRAINT [PK__NhaCungC__3A185DEB5ACF9149] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [nvarchar](30) NOT NULL,
	[MaTK] [nvarchar](30) NULL,
	[TenNV] [nvarchar](255) NOT NULL,
	[DiaChi] [nvarchar](255) NOT NULL,
	[SDT] [nvarchar](255) NULL,
	[ChucVu] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Luong] [nvarchar](100) NULL,
 CONSTRAINT [PK__NhanVien__2725D70A2A2CD53F] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPhamGiay]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPhamGiay](
	[MaSPG] [nvarchar](30) NOT NULL,
	[MaLG] [nvarchar](30) NULL,
	[MaNCC] [nvarchar](30) NULL,
	[TenGiay] [nvarchar](255) NOT NULL,
	[Img] [nvarchar](255) NOT NULL,
	[TonKho] [int] NULL,
	[GiaMua] [nvarchar](50) NULL,
	[GiaBan] [nvarchar](50) NULL,
 CONSTRAINT [PK__SanPhamG__318E25F2F481E078] PRIMARY KEY CLUSTERED 
(
	[MaSPG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SizeGiay]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SizeGiay](
	[MaSPG] [nvarchar](30) NOT NULL,
	[KichThuoc] [int] NOT NULL,
	[SoLuong] [int] NULL CONSTRAINT [DF_SizeGiay_SoLuong]  DEFAULT ((0)),
 CONSTRAINT [PK_SizeGiay] PRIMARY KEY CLUSTERED 
(
	[MaSPG] ASC,
	[KichThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 06/01/2024 6:28:40 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTK] [nvarchar](30) NOT NULL,
	[TenTK] [nvarchar](255) NOT NULL,
	[MatKhau] [nvarchar](255) NOT NULL,
	[PhanQuyen] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__TaiKhoan__2725007090BE56D2] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB1', N'SPG1', 10)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB2', N'SPG7', 2)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB2', N'SPG8', 5)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB3', N'SPG4', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB3', N'SPG5', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB3', N'SPG9', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB4', N'SPG4', 3)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB5', N'SPG2', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB5', N'SPG4', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB5', N'SPG5', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB6', N'SPG1', 1)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB6', N'SPG3', 2)
INSERT [dbo].[ChiTietHoaDonBan] ([MaHDB], [MaSPG], [SoLuong]) VALUES (N'HDB6', N'SPG7', 1)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM1', N'SPG1', 375)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM1', N'SPG2', 333)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM1', N'SPG3', 357)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM2', N'SPG4', 274)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM2', N'SPG5', 276)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM2', N'SPG6', 242)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM3', N'SPG7', 158)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM3', N'SPG8', 160)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM3', N'SPG9', 148)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM4', N'SPG10', 191)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM4', N'SPG11', 189)
INSERT [dbo].[ChiTietHoaDonMua] ([MaHDM], [MaSPG], [SoLuong]) VALUES (N'HDM4', N'SPG12', 150)
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB1', N'NV1', CAST(N'2024-01-01' AS Date), N'12.000.000 đ')
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB2', N'NV1', CAST(N'2024-01-01' AS Date), N'13.850.000 đ')
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB3', N'NV1', CAST(N'2024-01-02' AS Date), N'2.150.000 đ')
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB4', N'NV1', CAST(N'2024-01-02' AS Date), N'4.850.000 đ')
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB5', N'NV1', CAST(N'2024-01-02' AS Date), N'2.900.000 đ')
INSERT [dbo].[HoaDonBan] ([MaHDB], [MaNV], [NgayBan], [TongTien]) VALUES (N'HDB6', N'NV1', CAST(N'2024-01-02' AS Date), N'5.400.000 đ')
INSERT [dbo].[HoaDonMua] ([MaHDM], [MaNV], [NgayMua], [TongTien]) VALUES (N'HDM1', N'NV1', CAST(N'2023-12-27' AS Date), N'549.540.000 đ')
INSERT [dbo].[HoaDonMua] ([MaHDM], [MaNV], [NgayMua], [TongTien]) VALUES (N'HDM2', N'NV1', CAST(N'2023-12-27' AS Date), N'292.500.000 đ')
INSERT [dbo].[HoaDonMua] ([MaHDM], [MaNV], [NgayMua], [TongTien]) VALUES (N'HDM3', N'NV1', CAST(N'2023-12-27' AS Date), N'204.840.000 đ')
INSERT [dbo].[HoaDonMua] ([MaHDM], [MaNV], [NgayMua], [TongTien]) VALUES (N'HDM4', N'NV1', CAST(N'2023-12-27' AS Date), N'329.380.000 đ')
INSERT [dbo].[HoaDonMua] ([MaHDM], [MaNV], [NgayMua], [TongTien]) VALUES (N'HDM5', NULL, CAST(N'2024-01-06' AS Date), NULL)
INSERT [dbo].[LoaiGiay] ([MaLG], [Ten]) VALUES (N'LG01', N'Giày thể thao')
INSERT [dbo].[LoaiGiay] ([MaLG], [Ten]) VALUES (N'LG02', N'Giày công sở')
INSERT [dbo].[LoaiGiay] ([MaLG], [Ten]) VALUES (N'LG03', N'Giày đế  bằng')
INSERT [dbo].[LoaiGiay] ([MaLG], [Ten]) VALUES (N'LG04', N'Giày cao gót')
INSERT [dbo].[LoaiGiay] ([MaLG], [Ten]) VALUES (N'LG05', N'Giày bốtt')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC1', N'Công ty TNHH Thương mại Dịch vụ ABC', N'123 đường Trần Hưng Đạo, Quận 1, TP.HCM', N'02812345678')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC2', N'Công ty TNHH Sản xuất XYZ', N'456 đường Lê Duẩn, Quận 3, TP.HCM', N'02887654321')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC3', N'Công ty TNHH 1 thành viên LMN', N'789 đường Nguyễn Trãi, Quận 5, TP.HCM', N'02813572468')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC4', N'Công ty CP Đầu tư PQR', N'159 đường 3/2, Quận 10, TP.HCM', N'02886427531')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC5', N'Công ty TNHH MTV Vận tải STU', N'753 đường Cách Mạng Tháng Tám, Quận Tân Bình, TP.HCM', N'02824681357')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC6', N'Công ty CP Xây dựng GHI', N'951 đường Lý Thường Kiệt, Quận Tân Phú, TP.HCM', N'02875318642')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC7', N'Công ty TNHH Sản xuất KLM', N'357 đường Lê Hồng Phong, Quận 5, TP.HCM', N'02886429753')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT]) VALUES (N'NCC8', N'Công ty CP Thương mại DEF', N'864 đường Nguyễn Kiệm, Quận Phú Nhuận, TP.HCM', N'028 9753 8642')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV1', N'TK1', N'Lê Văn Thông', N'Hoàng mai - Hà nội', N'0123456789', N'Quản lý cửa hàng trưởng', N'minh02227227@gmail.com', N'100.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV2', N'TK2', N'Nguyễn văn A', N'Cầu giấy - Hà Nội', N'0123456781', N'Nhân viên bán hàng', N'nguyenvana@gmail.com', N'10.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV3', NULL, N'Trần Thị B', N'Đống Đa - Hà Nội', N'0123456782', N'Nhân viên bán hàng', N'tranb@gmail.com', N'11.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV4', NULL, N'Lê Thị C', N'Hai Bà Trưng - Hà Nội', N'0123456783', N'Nhân viên kỹ thuật', N'lethic@gmail.com', N'12.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV6', NULL, N'Nguyễn Thị Phương Mai', N'TP. Hồ Chí Minh', N'043754386', N'Nhân viên bán hàng', N'nguyenthipMai123@gmail.com', N'10.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV7', NULL, N'Đặng Văn F', N'Long Biên - Hà Nội', N'0123456786', N'Nhân viên bán hàng', N'dangvanf@gmail.com', N'15.000.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV8', NULL, N'Bùi Thị G', N'Tây Hồ - Hà Nội', N'0123456787', N'Nhân viên kỹ thuật', N'buithig@gmail.com', N'12.100.000 đ')
INSERT [dbo].[NhanVien] ([MaNV], [MaTK], [TenNV], [DiaChi], [SDT], [ChucVu], [Email], [Luong]) VALUES (N'NV9', NULL, N'Lê Văn Thông', N'Hoàng Mai - Hà Nội', N'0436785634', N'Nhân viên bán hàng', N'tt5128809@gmail.com', N'20.000.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG1', N'LG03', N'NCC6', N'Giày Air Jordan 1 Low ‘Alternate Bred Toe’ Like Auth', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\jodan1.jpg', 424, N'520.000 đ', N'1.200.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG10', N'LG05', N'NCC3', N'Giày MLB Korea Chunky Liner Mid Denim New York Yankees Gray', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\mlb1.jpeg', 201, N'620.000 đ', N'1.900.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG11', N'LG04', N'NCC3', N'Giày MLB Chunky Liner Basic NY ‘Brown’ 3ASXCLB3N-50CGS', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\mlb2.jpg', 189, N'640.000 đ', N'2.100.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG12', N'LG02', N'NCC2', N'Giày MLB Chunky Liner Mid NY ‘Green’ 3ASXLMB3N-50GNS', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\mlb3.jpg', 150, N'600.000 đ', N'1.800.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG2', N'LG02', N'NCC8', N'Giày Nike Air Jordan 1 Retro High Dior Like Auth 99%', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\jodan2.jpg', 299, N'400.000 đ', N'1.000.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG3', N'LG03', N'NCC3', N'Giày Jordan 1 Retro Low Dior CN8608-002 Like Auth', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\jodan3.jpg', 355, N'620.000 đ', N'1.350.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG4', N'LG01', N'NCC3', N'Giày Nike Air Force 1 Louis Vuitton Brown Like Auth', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\force1.jpg', 264, N'300.000 đ', N'800.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG5', N'LG02', N'NCC2', N'Giày Nike Air Force 1 07 Low Dark Grey Metallic Gold', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\force2.jpeg', 274, N'420.000 đ', N'1.100.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG6', N'LG03', N'NCC4', N'Giày AF1 x Louis Vuitton White Brown', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\force3.jpeg', 242, N'390.000 đ', N'1.150.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG7', N'LG03', N'NCC3', N'Giày Adidas Yeezy 350 V2 Black Reflective Like Auth', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\yeezy1.jpg', 155, N'540.000 đ', N'1.500.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG8', N'LG03', N'NCC2', N'Giày adidas Yeezy Boost 350 V2 Cloud White', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\yeezy2.jpg', 154, N'340.000 đ', N'1.100.000 đ')
INSERT [dbo].[SanPhamGiay] ([MaSPG], [MaLG], [MaNCC], [TenGiay], [Img], [TonKho], [GiaMua], [GiaBan]) VALUES (N'SPG9', N'LG03', N'NCC3', N'Giày Adidas Yeezy Boost 350 V2 Israfilx', N'D:\STORE_SHOES\source\StoreShoe\StoreShoe\photos\yeezy3.jpg', 145, N'440.000 đ', N'1.350.000 đ')
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 38, 79)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 39, 28)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 40, 64)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 41, 92)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 42, 71)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG1', 43, 90)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 38, 36)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 39, 31)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 40, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 41, 33)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 42, 34)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG10', 43, 35)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 38, 31)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 39, 31)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 40, 31)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 41, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 42, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG11', 43, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 38, 21)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 39, 21)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 40, 21)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 41, 23)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 42, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG12', 43, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 38, 35)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 39, 54)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 40, 30)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 41, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 42, 76)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG2', 43, 72)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 38, 73)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 39, 62)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 40, 50)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 41, 67)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 42, 51)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG3', 43, 52)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 38, 35)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 39, 37)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 40, 51)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 41, 87)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 42, 21)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG4', 43, 33)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 38, 30)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 39, 54)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 40, 62)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 41, 28)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 42, 48)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG5', 43, 52)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 38, 32)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 39, 63)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 40, 42)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 41, 42)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 42, 21)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG6', 43, 42)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 38, 27)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 39, 28)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 40, 29)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 41, 22)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 42, 25)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG7', 43, 24)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 38, 27)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 39, 28)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 40, 23)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 41, 25)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 42, 23)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG8', 43, 28)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 38, 29)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 39, 22)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 40, 24)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 41, 23)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 42, 23)
INSERT [dbo].[SizeGiay] ([MaSPG], [KichThuoc], [SoLuong]) VALUES (N'SPG9', 43, 24)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenTK], [MatKhau], [PhanQuyen]) VALUES (N'TK1', N'admin', N'123', N'Admin')
INSERT [dbo].[TaiKhoan] ([MaTK], [TenTK], [MatKhau], [PhanQuyen]) VALUES (N'TK2', N'lVTTK2', N'123', N'Nhân viên')
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__NhaCungC__CA1930A505795E9D]    Script Date: 06/01/2024 6:28:40 CH ******/
ALTER TABLE [dbo].[NhaCungCap] ADD  CONSTRAINT [UQ__NhaCungC__CA1930A505795E9D] UNIQUE NONCLUSTERED 
(
	[SDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__NhanVien__CA1930A5933B0F5B]    Script Date: 06/01/2024 6:28:40 CH ******/
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [UQ__NhanVien__CA1930A5933B0F5B] UNIQUE NONCLUSTERED 
(
	[SDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaHDB__37A5467C] FOREIGN KEY([MaHDB])
REFERENCES [dbo].[HoaDonBan] ([MaHDB])
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan] CHECK CONSTRAINT [FK__ChiTietHo__MaHDB__37A5467C]
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaSPG__38996AB5] FOREIGN KEY([MaSPG])
REFERENCES [dbo].[SanPhamGiay] ([MaSPG])
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan] CHECK CONSTRAINT [FK__ChiTietHo__MaSPG__38996AB5]
GO
ALTER TABLE [dbo].[ChiTietHoaDonMua]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaHDM__2D27B809] FOREIGN KEY([MaHDM])
REFERENCES [dbo].[HoaDonMua] ([MaHDM])
GO
ALTER TABLE [dbo].[ChiTietHoaDonMua] CHECK CONSTRAINT [FK__ChiTietHo__MaHDM__2D27B809]
GO
ALTER TABLE [dbo].[ChiTietHoaDonMua]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaSPG__2E1BDC42] FOREIGN KEY([MaSPG])
REFERENCES [dbo].[SanPhamGiay] ([MaSPG])
GO
ALTER TABLE [dbo].[ChiTietHoaDonMua] CHECK CONSTRAINT [FK__ChiTietHo__MaSPG__2E1BDC42]
GO
ALTER TABLE [dbo].[HoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK__HoaDonBan__MaNV__32E0915F] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDonBan] CHECK CONSTRAINT [FK__HoaDonBan__MaNV__32E0915F]
GO
ALTER TABLE [dbo].[HoaDonMua]  WITH CHECK ADD  CONSTRAINT [FK__HoaDonMua__MaNV__276EDEB3] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDonMua] CHECK CONSTRAINT [FK__HoaDonMua__MaNV__276EDEB3]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK__NhanVien__MaTK__1367E606] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TaiKhoan] ([MaTK])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK__NhanVien__MaTK__1367E606]
GO
ALTER TABLE [dbo].[SanPhamGiay]  WITH CHECK ADD  CONSTRAINT [FK__SanPhamGi__MaNCC__1CF15040] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[SanPhamGiay] CHECK CONSTRAINT [FK__SanPhamGi__MaNCC__1CF15040]
GO
ALTER TABLE [dbo].[SanPhamGiay]  WITH CHECK ADD  CONSTRAINT [FK__SanPhamGia__MaLG__1BFD2C07] FOREIGN KEY([MaLG])
REFERENCES [dbo].[LoaiGiay] ([MaLG])
GO
ALTER TABLE [dbo].[SanPhamGiay] CHECK CONSTRAINT [FK__SanPhamGia__MaLG__1BFD2C07]
GO
ALTER TABLE [dbo].[SizeGiay]  WITH CHECK ADD  CONSTRAINT [FK__SizeGiay__MaSPG__22AA2996] FOREIGN KEY([MaSPG])
REFERENCES [dbo].[SanPhamGiay] ([MaSPG])
GO
ALTER TABLE [dbo].[SizeGiay] CHECK CONSTRAINT [FK__SizeGiay__MaSPG__22AA2996]
GO
