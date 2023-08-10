use QLSV

go
create procedure SP_INS_ENCRYPT_SINHVIEN (@MASV nvarchar(20)
										,@HOTEN nvarchar(100)
										,@NGAYSINH datetime
										,@DIACHI nvarchar(200)
										,@MALOP varchar(20)
										,@TENDN nvarchar(100)
										,@MATKHAU varbinary(MAX))
as
	insert SINHVIEN(MASV,HOTEN,NGAYSINH,DIACHI,MALOP,TENDN,MATKHAU)
	values (@MASV,@HOTEN,@NGAYSINH,@DIACHI,@MALOP,@TENDN,@MATKHAU)

go
create procedure SP_INS_ENCRYPT_NHANVIEN (@MANV varchar(20)
										,@HOTEN nvarchar(100)
										,@EMAIL varchar(20)
										,@LUONG varbinary(200)
										,@TENDN nvarchar(100)
										,@MATKHAU varbinary(MAX))
as
	insert NHANVIEN(MANV,HOTEN,EMAIL,LUONG,TENDN,MATKHAU)
	values (@MANV,@HOTEN,@EMAIL,@LUONG,@TENDN,@MATKHAU)

go
create procedure SP_SEL_ENCRYPT_NHANVIEN
as
	select MANV,HOTEN,EMAIL,LUONG from NHANVIEN

.

