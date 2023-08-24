using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Ogep
    {
        public int Id { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public string? Diachi { get; set; }
        public int? Gia { get; set; }
        public string? Sdt { get; set; }
        public int? Trangthai { get; set; }
        public int? Doituongthue { get; set; }
        public DateTime? Thoigian { get; set; }
        public string? Hinhanh { get; set; }
        public int? Clicks { get; set; }

        public virtual Doituong? DoituongthueNavigation { get; set; }
    }
}
