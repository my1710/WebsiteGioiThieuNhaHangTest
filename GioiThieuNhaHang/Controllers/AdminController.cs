using GioiThieuNhaHang.Data;
using GioiThieuNhaHang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(AppDbContext context, ILogger<AdminController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // === Mặc định chuyển vào Dashboard nếu đã đăng nhập ===
    public IActionResult Index()
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        return RedirectToAction("Dashboard");
    }

    // === Đăng nhập ===
    // Hiển thị form đăng nhập (GET)
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(AdminLogin model)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("Nhập: " + model.Username + " / " + model.Password);

            //var admin = _context.AdminUsers.FirstOrDefault(x => x.Username == model.Username);
            var admin = _context.AdminUsers
      .FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower());

            if (admin == null)
            {
                ModelState.AddModelError("", "❌ Không tìm thấy tên đăng nhập.");
            }
            //else if (admin.PasswordHash != model.Password)
            else if (admin.Password != model.Password)
            {
                ModelState.AddModelError("", "❌ Mật khẩu không đúng.");
            }
            else
            {
                // Đăng nhập thành công
                HttpContext.Session.SetInt32("AdminId", admin.IdAD);
                HttpContext.Session.SetString("AdminUsername", admin.Username);
                HttpContext.Session.SetString("AdminRole", admin.Role);
                return RedirectToAction("Dashboard");
            }
        }

        return View(model);
    }



    // === Đăng xuất ===
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    // === Trang chính sau khi đăng nhập ===
    public IActionResult Dashboard()
    {
        var adminId = HttpContext.Session.GetInt32("AdminId");
        if (adminId == null)
        {
            return RedirectToAction("Login");
        }

        return View(); // Có View tên Dashboard.cshtml?
    }


    //========================

    // === QUẢN LÝ MÓN ĂN ===

    //========================

    public IActionResult ManageMonAn()
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var monanList = _context.MonAn.ToList();
        return View(monanList);

    }
 
    public IActionResult CreateMonAn()
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");
        ViewBag.LoaiMon = new SelectList(_context.LoaiMon, "IdLoai", "Name");
        return View();

    }

    [HttpPost]
    public IActionResult CreateMonAn(MonAn monAn)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");


        if (ModelState.IsValid)
        {
            _context.MonAn.Add(monAn);
            _context.SaveChanges();
            return RedirectToAction("ManageMonAn");
        }
        return View(monAn);
    }
 
    public IActionResult EditMonAn(int id)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");
        ViewBag.LoaiMon = new SelectList(_context.LoaiMon, "IdLoai", "Name");

        var item = _context.MonAn.Find(id);
        if (item == null)// return NotFound();
        {
            _logger.LogWarning("❌ Không tìm thấy món ăn với Id = {id}", id);
            return NotFound();
        }

        _logger.LogInformation("📝 Đang sửa món ăn Id = {id}", id);
        return View(item);
    }

    [HttpPost]
    public IActionResult EditMonAn(MonAn monAn)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        if (ModelState.IsValid)
        {
            _context.MonAn.Update(monAn);
            var affected = _context.SaveChanges();
            _logger.LogInformation("✅ Đã cập nhật món ăn Id = {id}, Số dòng ảnh hưởng = {affected}", monAn.IdMonAn, affected);
            return RedirectToAction("ManageMonAn");
        }

        _logger.LogWarning("⚠️ ModelState không hợp lệ khi sửa món ăn Id = {id}", monAn.IdMonAn);
        return View(monAn);
    }

    //[HttpPost]
    public IActionResult DeleteMonAn(int id)
    {
        Console.WriteLine($"🚀 Gọi DeleteMonAn với id = {id}");
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var item = _context.MonAn.Find(id);
        if (item != null)
        {
            _context.MonAn.Remove(item);
            _context.SaveChanges();
        }
        return RedirectToAction("ManageMonAn");
    }



    //========================
    //
    // === QUẢN LÝ ĐẶT BÀN ===
    //
    //========================

    public IActionResult ManageDatBan()
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var datbanList = _context.DatBan.ToList();
        return View(datbanList);
    }
    [HttpPost]
    public IActionResult ConfirmDatBan(int id)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var item = _context.DatBan.Find(id);
        if (item != null)
        {
            item.DaXacNhan = true;
            item.TrangThai = "Đã xác nhận";
            var affected = _context.SaveChanges();
            _logger.LogInformation("✅ Đã xác nhận đặt bàn Id = {id}, Số dòng ảnh hưởng = {affected}", id, affected);
        }
        else
        {
            _logger.LogWarning("❌ Không tìm thấy đặt bàn Id = {id} để xác nhận", id);
        }

        return RedirectToAction("ManageDatBan");
    }
    [HttpPost]
    public IActionResult DeleteDatBan(int id)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var item = _context.DatBan.Find(id);
        if (item != null)
        {
            _context.DatBan.Remove(item);
            var affected = _context.SaveChanges();
            _logger.LogInformation("🗑️ Đã xóa đặt bàn Id = {id}, Số dòng ảnh hưởng = {affected}", id, affected);
        }
        else
        {
            _logger.LogWarning("❌ Không tìm thấy đặt bàn Id = {id} để xóa", id);
        }
        return RedirectToAction("ManageDatBan");
    }
    public IActionResult Test()
    {
        return Content("Trang quản trị hoạt động!");
    }

}
