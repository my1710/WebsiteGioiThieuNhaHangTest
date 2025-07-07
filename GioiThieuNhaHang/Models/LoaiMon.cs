using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class LoaiMon
    {
        [Key]
        public int IdLoai { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<MonAn> MonAns { get; set; }
    }

}
