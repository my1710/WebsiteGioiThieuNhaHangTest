using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class AdminLogs
    {
      [Key]
        public int LogID { get; set; }
        public int IdAD { get; set; }
        public string HanhDong { get; set; }
        public DateTime ThoiGian { get; set; } = DateTime.Now;

        // Navigation
        public AdminUser AdminUser { get; set; }
    }

}
