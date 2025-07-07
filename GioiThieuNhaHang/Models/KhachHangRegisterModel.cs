using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class KhachHangRegisterModel
    {

        [Required]
        public string HoTen { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Required]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không đúng")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
