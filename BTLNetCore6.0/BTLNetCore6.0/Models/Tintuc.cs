using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Tintuc
    {
        public Tintuc()
        {
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int Id { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public string? Hinhanh { get; set; }
        public int? LoaitinId { get; set; }
        public int? TaikhoanId { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string? Sdt { get; set; }
        public int? Clicks { get; set; }
        public int? Guixe { get; set; }
        public string? Anhnguoidangs { get; set; }
        public string? Tennguoidang { get; set; }
        public int? Gia { get; set; }

        public virtual Loaitin? Loaitin { get; set; }
        public virtual Taikhoan? Taikhoan { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
