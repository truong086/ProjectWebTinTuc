using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace BTLNetCore6._0.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class AdminOgepsController : Controller
    {
        private readonly webtintucContext _context;

        public AdminOgepsController(webtintucContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOgeps
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            var lsOgep = _context.Ogeps.AsNoTracking().ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                lsOgep = lsOgep.Where(x => x.Tieude.Contains(name)).ToPagedList(page, pageSize);
            }

            return View(lsOgep);
            //var webtintucContext = _context.Ogeps.Include(o => o.DoituongthueNavigation);
            //return View(await webtintucContext.ToListAsync());
        }

        // GET: Admin/AdminOgeps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogeps == null)
            {
                return NotFound();
            }

            var ogep = await _context.Ogeps
                .Include(o => o.DoituongthueNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogep == null)
            {
                return NotFound();
            }

            return View(ogep);
        }

        // GET: Admin/AdminOgeps/Create
        public IActionResult Create()
        {
            ViewData["Doituongthue"] = new SelectList(_context.Doituongs, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminOgeps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tieude,Noidung,Diachi,Gia,Sdt,Trangthai,Doituongthue,Thoigian,Hinhanh")] Ogep ogep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Doituongthue"] = new SelectList(_context.Doituongs, "Id", "Id", ogep.Doituongthue);
            return View(ogep);
        }

        // GET: Admin/AdminOgeps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogeps == null)
            {
                return NotFound();
            }

            var ogep = await _context.Ogeps.FindAsync(id);
            if (ogep == null)
            {
                return NotFound();
            }
            ViewData["Doituongthue"] = new SelectList(_context.Doituongs, "Id", "Id", ogep.Doituongthue);
            return View(ogep);
        }

        // POST: Admin/AdminOgeps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tieude,Noidung,Diachi,Gia,Sdt,Trangthai,Doituongthue,Thoigian,Hinhanh")] Ogep ogep)
        {
            if (id != ogep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgepExists(ogep.Id))
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
            ViewData["Doituongthue"] = new SelectList(_context.Doituongs, "Id", "Id", ogep.Doituongthue);
            return View(ogep);
        }

        // GET: Admin/AdminOgeps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogeps == null)
            {
                return NotFound();
            }

            var ogep = await _context.Ogeps
                .Include(o => o.DoituongthueNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogep == null)
            {
                return NotFound();
            }

            return View(ogep);
        }

        // POST: Admin/AdminOgeps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogeps == null)
            {
                return Problem("Entity set 'webtintucContext.Ogeps'  is null.");
            }
            var ogep = await _context.Ogeps.FindAsync(id);
            if (ogep != null)
            {
                _context.Ogeps.Remove(ogep);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgepExists(int id)
        {
          return (_context.Ogeps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
