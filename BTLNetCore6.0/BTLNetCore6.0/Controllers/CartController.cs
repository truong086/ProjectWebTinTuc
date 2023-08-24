using BTLNetCore6._0.AddCart;
using BTLNetCore6._0.Entity;
using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTLNetCore6._0.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly webtintucContext _context;
        //private readonly HttpContextAccessor _httpContextAccessor;
        public CartController(webtintucContext context/*, HttpContextAccessor httpContextAccessor*/)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }
        // Hàm này lấy về tất cả sản phẩm trong giỏ hàng
        public List<CartItem> danhsachCart()
        {
            // Câu lệnh này là lấy ra tất cả sản phẩm trong trong giỏ hàng thông qua biến ("giohang") được lưu vào session
            var data = HttpContext.Session.Get<List<CartItem>>("giohang");

            // Kiểm tra nếu nếu giỏ hàng rỗng thì khởi tạo 1 giỏ hàng mới
            if (data == null)
            {
                data = new List<CartItem>();
            }

            return data;
        }
        public IActionResult Index()
        {
            return View(danhsachCart());
        }

        public IActionResult hienthi()
        {
            var datas = from b in _context.Sanphams select b;
            return View(datas);
        }
        public IActionResult AddToCart(int id)
        {
            // Lấy ra giỏ hàng ở trên
            var myCart = danhsachCart();

            // Kiểm tra xem "id" được chuyền vào đã tồn tại trong giỏ hàng chưa
            var item = myCart.SingleOrDefault(x => x.Mahh == id);
            if(item == null) // Kiểm tra nếu "id" chuyền vào chưa tồn tại trong giỏ hàng
            {
                // Câu lệnh này là lấy ra sản phẩm theo id được chuyền để add vào giỏ hàng
                var tintuc = _context.Tintucs.SingleOrDefault(x => x.Id == id);
                item = new CartItem { 
                    Mahh = id,
                    tieude = tintuc.Tieude,
                    gia = tintuc.Gia.GetValueOrDefault(),
                    soluong = tintuc.Guixe.GetValueOrDefault(),
                    hinhanh = tintuc.Hinhanh
                };
                myCart.Add(item);
            }
            else // Nếu đã có trong giỏ hàng thì tăng số lượng lên
            {
                item.soluong += 1;
            }
            // Sau khi cập nhật lại giỏ hàng xong thì phải "Set" lại dữ liệu cho giỏ hàng
            // Chuyền dữ liệu từ biến "myCart" và trong biến "giohang"
            HttpContext.Session.Set("giohang", myCart);
            return RedirectToAction("Index", "Home"); // Chuyển lại sang phương thức để hiện thị giở hàng
        }

        public IActionResult thanhtoan()
        {
            //Đầu tiên, cần lấy cookie của người dùng bằng cách sử dụng đối tượng HttpContext như sau:
            //var cookie = HttpContext.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];

            //// Tiếp theo, bạn cần giải mã cookie thành đối tượng ClaimsPrincipal để lấy thông tin người dùng bằng cách sử dụng phương thức AuthenticationHttpContextExtensions.AuthenticateAsync của đối tượng HttpContext như sau:
            //var giaima = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = HttpContext.User as ClaimsPrincipal;
            //var ids = principal.Identity.Name;
            //int id1 = int.Parse(ids.ToString());

            //string userId = HttpContext.User.FindFirstValue(ClaimTypes.Role); // Lấy ra quyền của người dùng
            // Lấy ra UserId của người dùng đã đc lưu vào cookie
            string userId = HttpContext.User.FindFirstValue("UserId");
            if (userId != null)
            {
                // Lấy thông tin trong giỏ hàng
                var lsCart = HttpContext.Session.Get<List<CartItem>>("giohang");
                Order oders = new Order();
                // Gán dữ liệu cho bảng "Order"
                oders.Ten = "Don hang - " + DateTime.Now.ToString("yyyyMMddHHmmss");
                oders.Statuc = 1;
                oders.Nguoimua = int.Parse(userId);
                oders.Ngaytao = DateTime.Now;
                _context.Orders.Add(oders);
                // Lưu thông tin vào bảng "Order"
                _context.SaveChanges();

                // Lấy ra "id" của bảng "Order" vừa mới tạo để lưu vào bảng "OrderDetails"
                var order_id = oders.Id;

                List<OrderDetai> lsOrderDetail = new List<OrderDetai>();
                foreach (var item in lsCart)
                {
                    OrderDetai orderdetail = new OrderDetai();
                    orderdetail.Soluong = item.soluong;
                    orderdetail.OrderId = order_id;
                    orderdetail.Tongtien = item.tonggia; // Ép kiểu sang "int". Vì "item.tonggia" là kiểu float còn "orderdetail.Tongtien" là kiểu int, nên phải ép kiểu từ float sang int để lưu vào database
                    orderdetail.SanphamId = item.Mahh;
                    lsOrderDetail.Add(orderdetail); // Lưu dữ liệu vừa thêm vào List(lsOrderDetail)
                }

                _context.OrderDetais.AddRange(lsOrderDetail);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
