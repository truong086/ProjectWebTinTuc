using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTLNetCore6._0.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly webtintucContext _context;
        public LoginController(webtintucContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username, string password)
        {
            var check = _context.Taikhoans.FirstOrDefault(x => x.Username == username && x.Matkhau == password);
            if(check != null) {
                var claim = new List<Claim>
                {
                    // Sử dụng "ClaimTypes" để lưu trữ được các trường dữ liệu của người dùng khi đăng nhập vào hệ thống, hoặc có thể tự đặt 1 biến riêng để lưu trữ, ví dụ: Biến "UserId" để lưu trữ Id của người dùng khi đăng nhập vào
                    new Claim(ClaimTypes.NameIdentifier, check.Id.ToString()), // Chuyển "acc.Id.ToString()" sang dạng "ToString" nghĩa là dạng chuỗi thì mới lưu trữ đc Id của người dùng
                    new Claim(ClaimTypes.Role, check.Phanquyen.ToString()), // Lưu trữ quyền của người dùng vào cookie, sử dụng "ClaimTypes.Role"
                    // Khi người dùng đăng nhập vào sẽ lấy ra "id" của người dùng, và gán lại cho biến "UserId", và khi nào muốn lấy ra "id" của người dùng từ cookie từ chỉ cần gọi lại biến "UserId"
                    // Khi nào muốn lấy ra "id" của người dùng từ cookie thì gọi lại biến "UserId" bằng cách sử dụng: "string userId = HttpContext.User.FindFirstValue("UserId");"
                    new Claim("UserId", check.Id.ToString()),
                    new Claim(ClaimTypes.Name, check.Fullname)
                };
                /*
                 * Đầu tiên, một đối tượng ClaimsIdentity được tạo với các claims và 
                 * CookieAuthenticationDefaults.AuthenticationScheme. ClaimsIdentity 
                 * đại diện cho danh tính của người dùng, bao gồm các thông tin xác 
                 * thực và quyền hạn của họ.
                 * var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                 */
                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                //Tạo Claim Principal để lấy thông tin người dùng login vào
                /*
                Sau đó, một đối tượng ClaimsPrincipal được tạo với identity đã tạo trước đó. 
                ClaimsPrincipal đại diện cho người dùng hiện tại trong quá trình xác thực, 
                và nó bao gồm một hoặc nhiều ClaimsIdentity có thể được sử dụng để kiểm tra 
                và cấp quyền hạn.
                Đoạn code trên cho phép bạn tạo một ClaimsPrincipal với ClaimsIdentity đã được cung cấp,
                từ đó bạn có thể sử dụng ClaimsPrincipal để thực hiện xác thực và kiểm tra quyền hạn 
                trong ứng dụng ASP.NET Core của bạn.
                var principal = new ClaimsPrincipal(identity);
                 */
                var princiban = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princiban);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "<div class='alert alert-danger'>Đăng nhập sai</div>";
            }
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }


    }
}
