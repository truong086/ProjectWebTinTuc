using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Taikhoan
    {
        public Taikhoan()
        {
            Orders = new HashSet<Order>();
            Tintucs = new HashSet<Tintuc>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Matkhau { get; set; }
        public int? Phanquyen { get; set; }
        public string? Fullname { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Tintuc> Tintucs { get; set; }
    }
}
