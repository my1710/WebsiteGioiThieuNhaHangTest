using GioiThieuNhaHang.Data; // Đảm bảo đúng namespace chứa AppDbContext
using GioiThieuNhaHang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace GioiThieuNhaHang.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;
        public MenuController(AppDbContext context) => _context = context;

        public IActionResult Index(int? loaiId)
        {
            var loaiMons = _context.LoaiMon.ToList();
            var MonAn = _context.MonAn
                .Where(m => loaiId == null || m.IdLoai == loaiId)
                .Include(m => m.LoaiMon)
                .ToList();

            ViewBag.LoaiMons = loaiMons;
            return View(MonAn);
        }
        public IActionResult DanhSachMonAn()
        {
            var monAnList = _context.MonAn
                .Include(m => m.LoaiMon)  // nếu bạn muốn lấy tên loại
                .OrderByDescending(m => m.IdMonAn)
                .ToList();

            return View(monAnList);
        }

    }

}
