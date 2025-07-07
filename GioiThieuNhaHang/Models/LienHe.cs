using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class LienHe
    {
        [Key]
        public int IdLienHe { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string TinNhan { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }

}
