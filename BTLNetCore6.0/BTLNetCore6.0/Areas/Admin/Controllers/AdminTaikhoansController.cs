using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLNetCore6._0.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace BTLNetCore6._0.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AdminTaikhoansController : Controller
    {
        private readonly webtintucContext _context;
        public INotyfService _notyfService { get; }

        public AdminTaikhoansController(webtintucContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTaikhoans
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 2;
            var lstaikhoan = _context.Taikhoans.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                lstaikhoan = lstaikhoan.Where(x => x.Username.Contains(name)).ToPagedList(page, pageSize);
            }
            return View(lstaikhoan);
        }

        // GET: Admin/AdminTaikhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTaikhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Matkhau,Phanquyen,Fullname,Ngaytao")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Matkhau,Phanquyen,Fullname,Ngaytao")] Taikhoan taikhoan)
        {
            if (id != taikhoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikhoan);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Taikhoans == null)
            {
                return Problem("Entity set 'webtintucContext.Taikhoans'  is null.");
            }
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan != null)
            {
                _context.Taikhoans.Remove(taikhoan);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Delete thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TaikhoanExists(int id)
        {
          return (_context.Taikhoans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
