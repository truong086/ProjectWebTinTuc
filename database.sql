create database webtintuc
go

use webtintuc

create table loaitin(
	id int primary key identity(1,1),
	ten nvarchar(50),
	ngaytao datetime
)
go

create table taikhoan(
	id int primary key identity(1,1),
	username nvarchar(50),
	matkhau nvarchar(50),
	phanquyen int,
	fullname nvarchar(50),
	ngaytao datetime
)
go
select * from taikhoan
create table tintuc(
	id int primary key identity(1,1),
	tieude nvarchar(50),
	noidung nvarchar(500),
	hinhanh nvarchar(500),
	loaitin_id int references loaitin(id),
	taikhoan_id int references taikhoan(id),
	ngaytao date,
)
go
select * from tintuc
alter table tintuc
-- add guixe int
-- add anhnguoidangs nvarchar(Max)
add tennguoidang nvarchar(50)
-- add gia int

create table orders(
	id int primary key identity(1,1),
	nguoimua int references taikhoan(id),
	ten nvarchar(100),
	statuc int,
	ngaytao datetime
)
go
select * from orders
create table orderDetais(
	id int primary key identity(1,1),
	order_id int references orders(id),
	sanpham_id int references tintuc(id),
	soluong int,
	tongtien int
)
go
select * from orderDetais
drop table orderDetais
select * from tintuc
update tintuc set clicks = 1
alter table tintuc
add clicks int
add sdt nvarchar(20)
create table sanpham(
	id int primary key identity(1,1),
	tieude nvarchar(50),
	noidung nvarchar(200),
	hinhanh nvarchar(500),
	gia float
)
go

create table doituong(
	id int primary key identity(1,1),
	ten nvarchar(20),
	ngaythem datetime
)
go

create table ogep(
	id int primary key identity(1,1),
	tieude nvarchar(50),
	noidung nvarchar(Max),
	diachi nvarchar(200),
	gia int,
	sdt nvarchar(20),
	trangthai int,
	doituongthue int references doituong(id),
	thoigian datetime
)
go
alter table ogep
add clicks int
alter table ogep
add hinhanh nvarchar(500)
select * from ogep
create table datas(
	id int primary key identity(1,1),
	ten nvarchar(20)
)
go

drop table datas
select * from loaitin
create table hang(
	id int primary key identity(1,1),
	ten nvarchar(50)
)
go

drop table hang
update ogep set clicks = 0