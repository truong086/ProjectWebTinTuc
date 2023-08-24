using BTLNetCore6._0.Models;
using BTLNetCore6._0.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTLNetCore6._0.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly webtintucContext _context;

        public HomeController(ILogger<HomeController> logger, webtintucContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 10;

            // Câu lệnh này là lấy ra toàn bộ dữ liệu trong bảng "tintuc" với điều kiện(where) là "Tieude" phải khác null và sắp xếp theo kiểu giảm dần theo ngày tháng
            var lsTinTuc = _context.Tintucs.Where(x => x.Tieude != null).ToPagedList(page, 10);

            // Câu lệnh này là lấy ra toàn bộ dữ liệu trong bảng "loaitintuc"
            var lsLoaiTinTuc = _context.Loaitins.AsNoTracking().OrderByDescending(x => x.Ngaytao).ToPagedList(page, pageSize);

            var tintucTop1 = _context.Tintucs.AsNoTracking().OrderByDescending(x => x.Id).Take(3).ToPagedList(page, 3);
            
            var phongtro = _context.Tintucs.AsNoTracking().Where(x => x.LoaitinId == 1).ToPagedList(page, pageSize);

            var vanphong = _context.Tintucs.AsNoTracking().Where(x => x.LoaitinId == 2).ToPagedList(page, pageSize);

            var clicks = _context.Tintucs.AsNoTracking().OrderByDescending(x => x.Clicks);

            var top1 = _context.Tintucs.AsNoTracking().OrderByDescending(x => x.Id).Take(1).FirstOrDefault();

            var ogep = _context.Ogeps.AsNoTracking().OrderByDescending(x => x.Id).Take(3).ToList();
			IPagedList<Tintuc> tintucs = lsTinTuc.ToPagedList(page, pageSize);
            IPagedList<Tintuc> tintucTop1s = tintucTop1.ToPagedList(page, pageSize);
            IPagedList<Loaitin> lsLoaiTinTucs = lsLoaiTinTuc.ToPagedList(page, pageSize);
            IPagedList<Tintuc> phongtros1 = phongtro.ToPagedList(page, pageSize);
            IPagedList<Tintuc> vanphongs1 = vanphong.ToPagedList(page, pageSize);
            List<Tintuc> clicks1 = clicks.ToList();

            ViewData["AllTinTuc"] = tintucs;
            ViewData["tintuctop1"] = tintucTop1s;
            ViewData["loaitin"] = lsLoaiTinTucs;
            ViewData["phongtros"] = phongtros1;
            ViewData["vanphongs"] = vanphongs1;
            ViewData["click"] = clicks1;
            ViewBag.top1 = top1;
            ViewBag.oghep = ogep;


			return View(lsTinTuc);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}