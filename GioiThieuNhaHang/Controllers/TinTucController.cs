using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GioiThieuNhaHang.Data;     // DbContext
using GioiThieuNhaHang.Models; 

namespace GioiThieuNhaHang.Controllers
{
    public class TinTucController : Controller
    {
        private readonly AppDbContext _context;

        // Inject DbContext qua constructor
        public TinTucController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách tin tức (mới nhất lên đầu)
        public IActionResult Index()
        {
            var tinTucs = _context.TinTuc
                                  .OrderByDescending(t => t.NgayTao)
                                  .ToList();

            return View(tinTucs);
        }
    }
}
