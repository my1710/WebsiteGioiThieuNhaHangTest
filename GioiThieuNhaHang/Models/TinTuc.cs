using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class TinTuc
    {
        [Key]
        public int IdTinTuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }

}
