using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTLNetCore6._0.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")] // Định nghĩa những người có quyền là "1" thì mới vào được trang này
    public class HomeController : Controller
    {
        //[Authorize(Roles = "1")] // Hoặc để "[Authorize(Roles = "1")]" vào từng phương thức trong controller
        public IActionResult Index()
        {
            return View();
        }
    }
}
