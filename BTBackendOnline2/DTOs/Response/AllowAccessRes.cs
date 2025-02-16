using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BTBackendOnline2.DTOs.Response
{
    public class AllowAccessRes
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;
        public string AccessPropertiesJson { get; set; } = "[]";

        [JsonIgnore]
        [NotMapped]
        public List<string> AccessProperties
        {
            get => string.IsNullOrWhiteSpace(AccessPropertiesJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(AccessPropertiesJson) ?? new List<string>();
            set => AccessPropertiesJson = JsonSerializer.Serialize(value ?? new List<string>());
        }
    }
}
