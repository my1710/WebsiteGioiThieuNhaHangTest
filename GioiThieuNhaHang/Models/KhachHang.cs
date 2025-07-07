using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class KhachHang
    {
        [Key]
        public int IdKH { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}
