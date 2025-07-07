using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class AdminUser
    {
        [Key] // Bắt buộc phải có để EF nhận diện
        public int IdAD { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } //  dùng Password thay  PasswordHash
        //public string PasswordHash { get; set; }

        public string Role { get; set; }

        // Điều hướng đến bảng log
        public ICollection<AdminLogs> Logs { get; set; }

        // Điều hướng đến bảng vai trò
        public ICollection<AdminRoles> AdminRoles { get; set; }
    }
}
