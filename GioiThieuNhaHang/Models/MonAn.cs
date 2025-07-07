using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GioiThieuNhaHang.Models
{
    public class MonAn
    {
        [Key]
        public int IdMonAn { get; set; }
        public string TenMon { get; set; }
        public string MoTa { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }

        // FK
        public int IdLoai { get; set; }
        public LoaiMon? LoaiMon { get; set; }
    }

}
