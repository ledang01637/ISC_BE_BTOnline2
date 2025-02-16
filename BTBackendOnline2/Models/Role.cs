using System.ComponentModel.DataAnnotations;

namespace BTBackendOnline2.Models
{
    public class Role
    {
        [Key]
        public int RoleId {  get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public virtual ICollection<AllowAccess>? AllowAccesses { get; set; }
        public virtual User? User { get; set; }
    }
}
