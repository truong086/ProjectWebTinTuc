namespace BTLNetCore6._0.Entity
{
    public class CartItem
    {
        public int Mahh { get; set; }
        public string tieude { get; set; }
        public string hinhanh { get; set; }
        public int soluong { get; set; }
        public int gia { get; set; }
        public int tonggia => soluong * gia;

    }
}
