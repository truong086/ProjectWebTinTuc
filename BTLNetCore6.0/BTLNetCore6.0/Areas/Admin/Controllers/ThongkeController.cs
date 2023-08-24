using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTLNetCore6._0.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ThongkeController : Controller
    {
        private readonly webtintucContext _context;
        public ThongkeController(webtintucContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var thongke = _context.OrderDetais.GroupBy(x => x.SanphamId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .ToList();
            var sanpham = _context.Tintucs.Where(t => thongke.Contains(t.Id)).ToList();
            var doanhthu = _context.OrderDetais.AsNoTracking().ToList();
            var thongkekhachhang = _context.Orders.GroupBy(x => x.Nguoimua)
                    .OrderByDescending(group => group.Count())
                    .Select(group => group.Key)
                    .ToList();
            var khachhang = _context.Taikhoans.Where(k => thongkekhachhang.Contains(k.Id)).ToList();

            ViewBag.doanhthu = doanhthu;
            ViewBag.khachhang = khachhang;
            return View(sanpham);
        }
    }
}
