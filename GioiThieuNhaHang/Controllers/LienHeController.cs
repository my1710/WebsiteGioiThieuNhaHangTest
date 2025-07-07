using Microsoft.AspNetCore.Mvc;
using GioiThieuNhaHang.Data;
using GioiThieuNhaHang.Models;
using GioiThieuNhaHang.Services;
using System;

namespace GioiThieuNhaHang.Controllers
{
    public class LienHeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public LienHeController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Hiển thị form liên hệ
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý gửi form và gửi email
        [HttpPost]
        public IActionResult Gui(LienHe model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                _context.LienHe.Add(model);
                _context.SaveChanges();

                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    string subject = "Nhà hàng ABC - Đã nhận liên hệ";
                    string body = $@"
                        <p>Chào {model.HoTen},</p>
                        <p>Chúng tôi đã nhận được tin nhắn của bạn:</p>
                        <blockquote>{model.TinNhan}</blockquote>
                        <p>Chúng tôi sẽ phản hồi bạn trong thời gian sớm nhất.</p>
                        <p>Trân trọng,<br/>Nhà hàng ABC</p>";

                    //  Gửi email
                    _emailService.SendEmailAsync(model.Email, subject, body);
                }

                return RedirectToAction("CamOn");
            }

            return View("Index", model);
        }

        // Trang cảm ơn
        public IActionResult CamOn()
        {
            return View();
        }
    }
}
