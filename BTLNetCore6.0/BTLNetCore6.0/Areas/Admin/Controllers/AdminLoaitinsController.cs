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
    public class AdminLoaitinsController : Controller
    {
        private readonly webtintucContext _context;
        public INotyfService _notyfService { get; }

        public AdminLoaitinsController(webtintucContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminLoaitins
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int pageSize = 2;
            page = page < 1 ? 1 : page;
            var lsLoaitin = _context.Loaitins.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                lsLoaitin = lsLoaitin.Where(x => x.Ten.Contains(name)).ToPagedList(page, pageSize);
            }
            return View(lsLoaitin);
        }

        // GET: Admin/AdminLoaitins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loaitins == null)
            {
                return NotFound();
            }

            var loaitin = await _context.Loaitins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaitin == null)
            {
                return NotFound();
            }

            return View(loaitin);
        }

        // GET: Admin/AdminLoaitins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminLoaitins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Ngaytao")] Loaitin loaitin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaitin);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(loaitin);
        }

        // GET: Admin/AdminLoaitins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loaitins == null)
            {
                return NotFound();
            }

            var loaitin = await _context.Loaitins.FindAsync(id);
            if (loaitin == null)
            {
                return NotFound();
            }
            return View(loaitin);
        }

        // POST: Admin/AdminLoaitins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Ngaytao")] Loaitin loaitin)
        {
            if (id != loaitin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaitin);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaitinExists(loaitin.Id))
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
            return View(loaitin);
        }

        // GET: Admin/AdminLoaitins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loaitins == null)
            {
                return NotFound();
            }

            var loaitin = await _context.Loaitins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaitin == null)
            {
                return NotFound();
            }

            return View(loaitin);
        }

        // POST: Admin/AdminLoaitins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loaitins == null)
            {
                return Problem("Entity set 'webtintucContext.Loaitins'  is null.");
            }
            var loaitin = await _context.Loaitins.FindAsync(id);
            if (loaitin != null)
            {
                _context.Loaitins.Remove(loaitin);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Delete thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool LoaitinExists(int id)
        {
          return (_context.Loaitins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
