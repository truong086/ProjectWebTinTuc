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
    public class AdminTintucsController : Controller
    {
        private readonly webtintucContext _context;
        public INotyfService _notyfService { get; }

        public AdminTintucsController(webtintucContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTintucs
        [HttpGet]
        public async Task<IActionResult> Index(string name, int? to, int? from, int page = 1) // "name" này sẽ nhận dữ liệu từ thẻ input trong from gửi lên, thẻ input đấy có tên là "name"
        {
            //var webtintucContext = _context.Tintucs.Include(t => t.Loaitin).Include(t => t.Taikhoan);

            // Sử dụng lệnh truy vấn giống sql server để truy vấn
            //var lsTintuc = from b in _context.Tintucs select b; // Câu lệnh truy vấn lấy ra tất cả danh sách tin tức

            // Kiểm tra nếu trang hiện tại chuyền vào mà nhỏ hơn 1 thì sẽ trở về trang 1
            page = page < 1 ? 1 : page;
            var pageSize = 2;
            var lsTintuc = _context.Tintucs.ToPagedList(page, pageSize); // "ToPagedList()" là thư viện để phân trang
            // Kiểm tra nếu "name" chuyền vào mà không rỗng thì sẽ bắt đầu tìm kiếm
            if (!string.IsNullOrEmpty(name))
            {
                if (to != null && from != null)
                {
                    // Nếu "name", "to", "from" chuyền vào mà không bị rỗng thì tìm kiếm sử dụng mệnh đề "where" để tìm kiếm, và sử dụng "Contains()" để tìm kiếm ký tự
                    // "to", "from" ở đây nghĩa là giới hạn từ đâu đến đâu, "to": nghĩa là bắt đầu từ đâu, "from": nghĩa là đến giới hạn ở đâu
                    lsTintuc = lsTintuc.Where(x => x.Tieude.Contains(name) && x.LoaitinId >= to && x.LoaitinId <= from).ToPagedList(page, pageSize);
                }
                else
                {
                    // Nếu "name" chuyền vào mà không bị rỗng thì tìm kiếm sử dụng mệnh đề "where" để tìm kiếm, và sử dụng "Contains()" để tìm kiếm ký tự
                    lsTintuc = lsTintuc.Where(x => x.Tieude.Contains(name)).ToPagedList(page, pageSize);
                }

            }
            else
            {
                if (to != null && from != null)
                {
                    // Nếu "name", "to", "from" chuyền vào mà không bị rỗng thì tìm kiếm sử dụng mệnh đề "where" để tìm kiếm, và sử dụng "Contains()" để tìm kiếm ký tự
                    // "to", "from" ở đây nghĩa là giới hạn từ đâu đến đâu, "to": nghĩa là bắt đầu từ đâu, "from": nghĩa là đến giới hạn ở đâu
                    lsTintuc = lsTintuc.Where(x => x.LoaitinId >= to && x.LoaitinId <= from).ToPagedList(page, pageSize);
                }
                
            }

            //return View(await webtintucContext.ToListAsync());
            return View(lsTintuc);

        }

        // Đây là hàm tìm kiếm theo danh mục
        //public IActionResult Index(int? loaitinId) // "int?" nghĩa là có thể nhận giá trị null
        //{
        //    // Câu lệnh này nghĩa là: Nếu "loaitinId" được chuyền vào mà bằng null thì sẽ chỉ định cho "loaitinId" bằng 0
        //    //var ltId = loaitinId ?? 0;

        //    // Câu lệnh này nghĩa là: Nếu "loaitinId" được chuyền vào mà bằng null thì sẽ chỉ định cho "loaitinId" bằng 0
        //    loaitinId = loaitinId ?? 0;

        //    var lsLoatin = _context.Loaitins.ToList(); // Lấy ra danh sách loại tin

        //    // Thêm 1 dòng vào đầu danh sách là: "Hãy chọn thể loại tin tức"
        //    // "0" nghĩa là vị trí đầu tiên
        //    // Danh sách "lsLoaitin" này vừa được lấy ra ở bên trên, rồi insert thêm 1 dòng nữa vào đầu danh sách vừa đc lấy ra
        //    lsLoatin.Insert(0, new Loaitin { Id = 0, Ten = "-----------Hãy chọn thể loại tin tức---------" });

        //    // Đối số đầu vào của "SelectList" là 1 tập dữ liệu là: "lsLoatin", tham số thứ 2 là giá trị được chọn(Trường dữ liệu trong database) khi nào submit thì hệ thống sẽ đẩy trường "Id" này lên server, tham số thứ 3 là: Text "Ten", tham số thứ 4 là giá trị được chọn: "loaitinId" chính là tham số được chuyền từ ngoài vào khi tìm kiếm xong thì từ khóa vừa đc tìm kiếm đấy nó vẫn sẽ đc nổi ở trên ô tìm kiếm
        //    ViewBag.listLoaTin = new SelectList(lsLoatin, "Id", "Ten", loaitinId);  // Đoạn code này là: Mang danh mục của bảng "loaitin" để đẩy vào ViewBang 

        //    // Lấy ra danh sách tin tức theo danh mục
        //    var lsTinTuc = _context.Tintucs.Where(x => x.LoaitinId == loaitinId);
        //    return View(lsTinTuc.ToList());
        //}

        // GET: Admin/AdminTintucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .Include(t => t.Loaitin)
                .Include(t => t.Taikhoan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Create
        public IActionResult Create()
        {
            ViewData["LoaitinId"] = new SelectList(_context.Loaitins, "Id", "Id");
            ViewData["TaikhoanId"] = new SelectList(_context.Taikhoans, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminTintucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tieude,Noidung,Hinhanh,LoaitinId,TaikhoanId,Ngaytao, Sdt, Clicks, Guixe, Anhnguoidangs, Tennguoidang, Gia")] Tintuc tintuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tintuc);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaitinId"] = new SelectList(_context.Loaitins, "Id", "Id", tintuc.LoaitinId);
            ViewData["TaikhoanId"] = new SelectList(_context.Taikhoans, "Id", "Id", tintuc.TaikhoanId);
            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs.FindAsync(id);
            if (tintuc == null)
            {
                return NotFound();
            }
            ViewData["LoaitinId"] = new SelectList(_context.Loaitins, "Id", "Id", tintuc.LoaitinId);
            ViewData["TaikhoanId"] = new SelectList(_context.Taikhoans, "Id", "Id", tintuc.TaikhoanId);
            return View(tintuc);
        }

        // POST: Admin/AdminTintucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tieude,Noidung,Hinhanh,LoaitinId,TaikhoanId,Ngaytao, Sdt, Clicks, Guixe, Anhnguoidangs, Tennguoidang, Gia")] Tintuc tintuc)
        {
            if (id != tintuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tintuc);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucExists(tintuc.Id))
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
            ViewData["LoaitinId"] = new SelectList(_context.Loaitins, "Id", "Id", tintuc.LoaitinId);
            ViewData["TaikhoanId"] = new SelectList(_context.Taikhoans, "Id", "Id", tintuc.TaikhoanId);
            return View(tintuc);
        }

        // GET: Admin/AdminTintucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tintucs == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintucs
                .Include(t => t.Loaitin)
                .Include(t => t.Taikhoan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // POST: Admin/AdminTintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tintucs == null)
            {
                return Problem("Entity set 'webtintucContext.Tintucs'  is null.");
            }
            var tintuc = await _context.Tintucs.FindAsync(id);
            if (tintuc != null)
            {
                _context.Tintucs.Remove(tintuc);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Delete thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TintucExists(int id)
        {
          return (_context.Tintucs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
