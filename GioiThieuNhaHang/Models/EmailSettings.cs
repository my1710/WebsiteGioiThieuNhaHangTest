using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    namespace GioiThieuNhaHang.Services
    {
        public class EmailSettings
        {
            [Key]
            public string SenderEmail { get; set; }
            public string SenderName { get; set; }
            public string SenderPassword { get; set; }
            public string SmtpServer { get; set; }
            public int Port { get; set; }
        }
    }

}
