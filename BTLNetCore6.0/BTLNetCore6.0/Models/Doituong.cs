using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Doituong
    {
        public Doituong()
        {
            Ogeps = new HashSet<Ogep>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public DateTime? Ngaythem { get; set; }

        public virtual ICollection<Ogep> Ogeps { get; set; }
    }
}
