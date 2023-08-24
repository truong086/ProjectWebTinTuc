using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTLNetCore6._0.Controllers
{
    [Authorize]
    public class NguoithueController : Controller
    {
        private readonly webtintucContext _context;

        public NguoithueController(webtintucContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult nguoithue(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 1;
            var lsNguoithue = _context.Ogeps.AsNoTracking().Where(x => x.Tieude != null).ToPagedList(page, pageSize);
            return View(lsNguoithue);
        }

        [Route("/thongtin/{Tieude}-{Id}.html", Name = "Detail")]
        public IActionResult chitiet(int Id)
        {
            var lsOgep = _context.Ogeps.AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();
            if(lsOgep != null)
            {
                lsOgep = new Ogep
                {
                    Id = Id,
                    Tieude = lsOgep.Tieude,
                    Noidung = lsOgep.Noidung,
                    Gia = lsOgep.Gia,
                    Clicks = lsOgep.Clicks + 1,
                    Diachi = lsOgep.Diachi,
                    Sdt = lsOgep.Sdt,
                    Trangthai = lsOgep.Trangthai,
                    Doituongthue = lsOgep.Doituongthue,
                    Thoigian = lsOgep.Thoigian,
                    Hinhanh = lsOgep.Hinhanh
                };
                _context.Update(lsOgep);
                _context.SaveChanges();
            }
            var lienquan = _context.Ogeps.AsNoTracking().Where(x => x.Diachi.Contains(lsOgep.Diachi)).Take(5).ToList();
            var tinmoi = _context.Ogeps.AsNoTracking().Where(x => x.Id != Id).OrderByDescending(x => x.Thoigian).Take(5).ToList();
            ViewBag.lienquans = lienquan;
            ViewBag.tinmois = tinmoi;
            return View(lsOgep);
        }


        
    }
}
