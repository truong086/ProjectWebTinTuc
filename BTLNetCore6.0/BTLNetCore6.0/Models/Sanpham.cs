using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Sanpham
    {
        public int Id { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public string? Hinhanh { get; set; }
        public double? Gia { get; set; }
    }
}
