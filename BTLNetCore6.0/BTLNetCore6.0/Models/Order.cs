using System;
using System.Collections.Generic;

namespace BTLNetCore6._0.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int Id { get; set; }
        public int? Nguoimua { get; set; }
        public string? Ten { get; set; }
        public int? Statuc { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual Taikhoan? NguoimuaNavigation { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
