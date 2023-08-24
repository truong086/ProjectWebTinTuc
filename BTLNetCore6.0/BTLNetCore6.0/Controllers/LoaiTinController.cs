using BTLNetCore6._0.AddCart;
using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using X.PagedList;

namespace BTLNetCore6._0.Controllers
{
    [Authorize]
    public class LoaiTinController : Controller
    {
        private readonly webtintucContext _context;
        //private readonly HttpContextAccessor _httpContextAccessor;
        public LoaiTinController(webtintucContext context /*HttpContextAccessor httpContextAccessor*/)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/loaitin/{Id}", Name = "LoaiTin")]
        public IActionResult loaitin(string name, int Id, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 10;
            try
            {
                var loaitins = _context.Loaitins.AsNoTracking().SingleOrDefault(x => x.Id == Id);
                var tintuc = _context.Tintucs.AsNoTracking().Where(x => x.LoaitinId == loaitins.Id).OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
                var orderDetails = _context.OrderDetais.AsNoTracking().ToList();
                if (!string.IsNullOrEmpty(name))
                {
                    tintuc = tintuc.Where(x => x.Tieude.Contains(name)).ToPagedList(page, pageSize);
                }
                IPagedList<Tintuc> lsTinTuc = tintuc.ToPagedList(page, pageSize);
                ViewBag.OneLoaiTin = loaitins;
                ViewBag.orderDetails = orderDetails;
                return View(tintuc);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("/{TieuDe}-{Id}.html", Name = "details")]
        public IActionResult detail(int Id)
        {
            var clicks1 = _context.Tintucs.AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();
            if (clicks1 != null)
            {
                clicks1 = new Tintuc
                {
                    Id = Id,
                    Tieude = clicks1.Tieude,
                    Noidung = clicks1.Noidung,
                    Hinhanh = clicks1.Hinhanh,
                    LoaitinId = clicks1.LoaitinId,
                    TaikhoanId = clicks1.TaikhoanId,
                    Ngaytao = clicks1.Ngaytao,
                    Clicks = clicks1.Clicks + 1,
                    Sdt = clicks1.Sdt,
                    Anhnguoidangs = clicks1.Anhnguoidangs,
                    Gia = clicks1.Gia,
                    Guixe = clicks1.Guixe,
                    Tennguoidang = clicks1.Tennguoidang

                };
                _context.Update(clicks1);
                _context.SaveChanges();
            }
            try
            {
                var tintuc = _context.Tintucs.Include(x => x.Loaitin).FirstOrDefault(x => x.Id == Id);
                var lienquan = _context.Tintucs.AsNoTracking().Where(x => x.LoaitinId == tintuc.LoaitinId && x.Id != Id).ToList();
                if(tintuc == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                
                ViewBag.lienquans = lienquan;
                return View(tintuc);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
    }
}
