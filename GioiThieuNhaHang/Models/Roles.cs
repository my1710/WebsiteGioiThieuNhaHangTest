using System.ComponentModel.DataAnnotations;

namespace GioiThieuNhaHang.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        // Navigation
        public ICollection<AdminRoles> AdminRoles { get; set; }
    }

}
