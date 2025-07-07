using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class AdminLogin
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }


}
