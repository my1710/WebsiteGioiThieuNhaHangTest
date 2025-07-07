using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class DatBan
    {
        [Key]
        public int IdDatBan { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required]
        // public DateTime? ThoiGian { get; set; }
        [Display(Name = "Ngày đặt bàn")]
        [DataType(DataType.Date)] // Gợi ý Razor dùng input date
        //[Required(ErrorMessage = "Vui lòng chọn ngày đặt bàn")]
        public DateTime ThoiGian { get; set; }


        public int SoNguoi { get; set; }
        public string TrangThai { get; set; } = "Chờ xác nhận";
        public bool DaXacNhan { get; set; } //  nullable bool?

        public int IdKH { get; set; }
        // public KhachHang KhachHang { get; set; } // navigation property (optional)


    }

}
