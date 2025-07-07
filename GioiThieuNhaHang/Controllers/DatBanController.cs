using GioiThieuNhaHang.Models;
using Microsoft.AspNetCore.Mvc;
using GioiThieuNhaHang.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GioiThieuNhaHang.Controllers
{
    public class DatBanController : Controller
    {
        private readonly AppDbContext _context;

        public DatBanController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị form đặt bàn + đơn của tôi
        public IActionResult Index()
        {
            int? IdKH = HttpContext.Session.GetInt32("KhachHangId");

            if (IdKH != null)
            {
                ViewBag.DonCuaToi = _context.DatBan
                    .Where(d => d.IdKH == IdKH)
                    .OrderByDescending(d => d.ThoiGian)
                    .ToList();
            }
            else
            {
                ViewBag.DonCuaToi = new List<DatBan>();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DatBan(DatBan datBan)
        {
            int? IdKH = HttpContext.Session.GetInt32("KhachHangId");
            if (IdKH == null)
            {
                TempData["ReturnUrl"] = Url.Action("Index", "DatBan");
                TempData["ThongBao"] = "Bạn phải đăng nhập hoặc đăng ký trước khi đặt bàn!";
                return RedirectToAction("Login", "KhachHang");
            }


            if (ModelState.IsValid)
            {
                datBan.TrangThai = "Chờ xác nhận";
                datBan.IdKH = IdKH.Value;
                _context.DatBan.Add(datBan);
                _context.SaveChanges();

                TempData["ThongBao"] = "✔️ Đặt bàn thành công!";
                return RedirectToAction("Index");

            }

            // cập nhật lại danh sách đơn của tôi
            ViewBag.DonCuaToi = _context.DatBan
                .Where(d => d.IdKH == IdKH)
                .OrderByDescending(d => d.ThoiGian)
                .ToList();

            return View("Index");
        }
        //public IActionResult DonCuaToi()
        //{
        //    int? IdKH = HttpContext.Session.GetInt32("KhachHangId");
        //    if (IdKH == null)
        //    {
        //        TempData["ThongBao"] = "Bạn phải đăng nhập hoặc đăng ký trước!";
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var don = _context.DatBan
        //                .Where(d => d.IdKH == IdKH)
        //                .OrderByDescending(d => d.ThoiGian)
        //                .ToList();

        //    return View(don);
        //}

    }
}
