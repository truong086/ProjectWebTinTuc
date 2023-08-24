using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BTLNetCore6._0.Models
{
    public partial class webtintucContext : DbContext
    {
        public webtintucContext()
        {
        }

        public webtintucContext(DbContextOptions<webtintucContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doituong> Doituongs { get; set; } = null!;
        public virtual DbSet<Loaitin> Loaitins { get; set; } = null!;
        public virtual DbSet<Ogep> Ogeps { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetai> OrderDetais { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;
        public virtual DbSet<Taikhoan> Taikhoans { get; set; } = null!;
        public virtual DbSet<Tintuc> Tintucs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("User Id=sa;Password=1234;Server=LAPTOP-D5OKMO3H\\SQLEXPRESS;Database=webtintuc");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doituong>(entity =>
            {
                entity.ToTable("doituong");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaythem)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaythem");

                entity.Property(e => e.Ten)
                    .HasMaxLength(20)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Loaitin>(entity =>
            {
                entity.ToTable("loaitin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Ogep>(entity =>
            {
                entity.ToTable("ogep");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clicks).HasColumnName("clicks");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(200)
                    .HasColumnName("diachi");

                entity.Property(e => e.Doituongthue).HasColumnName("doituongthue");

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(500)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("sdt");

                entity.Property(e => e.Thoigian)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigian");

                entity.Property(e => e.Tieude)
                    .HasMaxLength(50)
                    .HasColumnName("tieude");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.DoituongthueNavigation)
                    .WithMany(p => p.Ogeps)
                    .HasForeignKey(d => d.Doituongthue)
                    .HasConstraintName("FK__ogep__doituongth__04E4BC85");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Nguoimua).HasColumnName("nguoimua");

                entity.Property(e => e.Statuc).HasColumnName("statuc");

                entity.Property(e => e.Ten)
                    .HasMaxLength(100)
                    .HasColumnName("ten");

                entity.HasOne(d => d.NguoimuaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Nguoimua)
                    .HasConstraintName("FK__orders__nguoimua__4F47C5E3");
            });

            modelBuilder.Entity<OrderDetai>(entity =>
            {
                entity.ToTable("orderDetais");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.SanphamId).HasColumnName("sanpham_id");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tongtien).HasColumnName("tongtien");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__orderDeta__order__625A9A57");

                entity.HasOne(d => d.Sanpham)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.SanphamId)
                    .HasConstraintName("FK__orderDeta__sanph__634EBE90");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.ToTable("sanpham");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(500)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(200)
                    .HasColumnName("noidung");

                entity.Property(e => e.Tieude)
                    .HasMaxLength(50)
                    .HasColumnName("tieude");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.ToTable("taikhoan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(50)
                    .HasColumnName("matkhau");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Phanquyen).HasColumnName("phanquyen");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.ToTable("tintuc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anhnguoidangs).HasColumnName("anhnguoidangs");

                entity.Property(e => e.Clicks).HasColumnName("clicks");

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Guixe).HasColumnName("guixe");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(500)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.LoaitinId).HasColumnName("loaitin_id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("date")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(500)
                    .HasColumnName("noidung");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("sdt");

                entity.Property(e => e.TaikhoanId).HasColumnName("taikhoan_id");

                entity.Property(e => e.Tennguoidang)
                    .HasMaxLength(50)
                    .HasColumnName("tennguoidang");

                entity.Property(e => e.Tieude)
                    .HasMaxLength(50)
                    .HasColumnName("tieude");

                entity.HasOne(d => d.Loaitin)
                    .WithMany(p => p.Tintucs)
                    .HasForeignKey(d => d.LoaitinId)
                    .HasConstraintName("FK__tintuc__loaitin___3A81B327");

                entity.HasOne(d => d.Taikhoan)
                    .WithMany(p => p.Tintucs)
                    .HasForeignKey(d => d.TaikhoanId)
                    .HasConstraintName("FK__tintuc__taikhoan__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
