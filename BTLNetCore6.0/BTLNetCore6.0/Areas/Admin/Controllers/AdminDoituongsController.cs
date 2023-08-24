using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLNetCore6._0.Models;
using X.PagedList;

namespace BTLNetCore6._0.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDoituongsController : Controller
    {
        private readonly webtintucContext _context;

        public AdminDoituongsController(webtintucContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminDoituongs
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            var lsDoituong = _context.Doituongs.AsNoTracking().Where(x => x.Ten != null).ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                lsDoituong = lsDoituong.Where(x => x.Ten.Contains(name)).ToPagedList(page, pageSize);
            }
            return View(lsDoituong);
              //return _context.Doituongs != null ? 
              //            View(await _context.Doituongs.ToListAsync()) :
              //            Problem("Entity set 'webtintucContext.Doituongs'  is null.");
        }

        // GET: Admin/AdminDoituongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doituongs == null)
            {
                return NotFound();
            }

            var doituong = await _context.Doituongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doituong == null)
            {
                return NotFound();
            }

            return View(doituong);
        }

        // GET: Admin/AdminDoituongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminDoituongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Ngaythem")] Doituong doituong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doituong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doituong);
        }

        // GET: Admin/AdminDoituongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doituongs == null)
            {
                return NotFound();
            }

            var doituong = await _context.Doituongs.FindAsync(id);
            if (doituong == null)
            {
                return NotFound();
            }
            return View(doituong);
        }

        // POST: Admin/AdminDoituongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Ngaythem")] Doituong doituong)
        {
            if (id != doituong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doituong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoituongExists(doituong.Id))
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
            return View(doituong);
        }

        // GET: Admin/AdminDoituongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doituongs == null)
            {
                return NotFound();
            }

            var doituong = await _context.Doituongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doituong == null)
            {
                return NotFound();
            }

            return View(doituong);
        }

        // POST: Admin/AdminDoituongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doituongs == null)
            {
                return Problem("Entity set 'webtintucContext.Doituongs'  is null.");
            }
            var doituong = await _context.Doituongs.FindAsync(id);
            if (doituong != null)
            {
                _context.Doituongs.Remove(doituong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoituongExists(int id)
        {
          return (_context.Doituongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
