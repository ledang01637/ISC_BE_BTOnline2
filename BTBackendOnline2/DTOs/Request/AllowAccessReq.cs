using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BTBackendOnline2.DTOs.Request
{
    public class AllowAccessReq
    {
        public int RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;

        [JsonIgnore]
        public string AccessPropertiesJson
        {
            get => JsonSerializer.Serialize(AccessProperties);
            set => AccessProperties = string.IsNullOrWhiteSpace(value)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(value) ?? new List<string>();
        }

        [NotMapped]
        public List<string> AccessProperties { get; set; } = new();

        
    }
}
