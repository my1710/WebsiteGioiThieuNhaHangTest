// Controllers/KhachHangController.cs
using Microsoft.AspNetCore.Mvc;
using GioiThieuNhaHang.Data;
using GioiThieuNhaHang.Models;
using System.Security.Cryptography;
using System.Text;

public class KhachHangController : Controller
{
    private readonly AppDbContext _context;

    public KhachHangController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Register(KhachHangRegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var kh = new KhachHang
            {
                HoTen = model.HoTen,
                Email = model.Email,
                MatKhau = model.MatKhau
            };

            _context.KhachHang.Add(kh);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("KhachHangId", kh.IdKH);
            HttpContext.Session.SetString("KhachHangEmail", kh.Email);

            if (TempData["ReturnUrl"] != null)
            {
                string returnUrl = TempData["ReturnUrl"].ToString();
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "DatBan");
        }

        return View(model);
    }


    [HttpPost]
    public IActionResult Login(KhachHangLoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var kh = _context.KhachHang.FirstOrDefault(k => k.Email == model.Email);
        if (kh == null)
        {
            TempData["ThongBao"] = "Bạn chưa có tài khoản, hãy đăng ký!";
            return RedirectToAction("Register");
        }

        if (kh.MatKhau != model.MatKhau)
        {
            ModelState.AddModelError("", "Mật khẩu không đúng.");
            return View(model);
        }
        // luuue= session
        HttpContext.Session.SetInt32("KhachHangId", kh.IdKH);
        HttpContext.Session.SetString("KhachHangEmail", kh.Email);

        // về trang ban đầu nếu có
        if (TempData["ReturnUrl"] != null)
        {
            string returnUrl = TempData["ReturnUrl"].ToString();
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "DatBan");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Xóa toàn bộ session của khách hàng
        TempData["ThongBao"] = "Bạn đã đăng xuất.";
        return RedirectToAction("Index", "DatBan"); // Chuyển về trang đặt bàn (hoặc Trang chủ nếu muốn)
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}
