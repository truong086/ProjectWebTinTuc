using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class OrderDetai
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? SanphamId { get; set; }
        public int? Soluong { get; set; }
        public int? Tongtien { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Tintuc? Sanpham { get; set; }
    }
}
