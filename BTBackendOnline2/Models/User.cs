using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace BTBackendOnline2.Models
{
    public class User
    {
        [Key]
        public int UserId {  get; set; }
        public int RoleId {  get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }

        public virtual Role? Role { get; set; }
    } 
}
