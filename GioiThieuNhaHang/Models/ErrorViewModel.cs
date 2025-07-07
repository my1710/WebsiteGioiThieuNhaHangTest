using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class ErrorViewModel
    {
        [Key]
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
