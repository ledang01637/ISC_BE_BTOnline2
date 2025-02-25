using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BTBackendOnline2.Models
{
    public class AllowAccess
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;
        public string AccessPropertiesJson { get; set; } = "[]";

        [NotMapped]
        public List<string> AccessProperties
        {
            get => string.IsNullOrWhiteSpace(AccessPropertiesJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(AccessPropertiesJson) 
                ?? new List<string>();

            set => AccessPropertiesJson = JsonSerializer.Serialize(value ?? new List<string>());
        }

        public virtual Role? Role { get; set; }
    }
}
