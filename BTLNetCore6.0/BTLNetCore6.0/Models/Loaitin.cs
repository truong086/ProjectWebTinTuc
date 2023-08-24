using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Loaitin
    {
        public Loaitin()
        {
            Tintucs = new HashSet<Tintuc>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual ICollection<Tintuc> Tintucs { get; set; }
    }
}
