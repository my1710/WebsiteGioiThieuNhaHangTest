using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class AdminRoles
    {
        [Key]
        public int IdAD { get; set; }
        public int RoleID { get; set; }

        // Navigation
        public AdminUser AdminUser { get; set; }
        public Roles Role { get; set; }
    }

}
