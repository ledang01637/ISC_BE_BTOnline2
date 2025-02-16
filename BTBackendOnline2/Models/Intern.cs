using System.ComponentModel.DataAnnotations;

namespace BTBackendOnline2.Models
{
    public class Intern
    {
        [Key]
        public int Id { get; set; }
        public string? InternName { get; set; } = string.Empty;
        public string? InternAddress { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? InternMail { get; set; } = string.Empty;
        public string? InternMailReplace { get; set; } = string.Empty;
        public string? University { get; set; } = string.Empty;
        public string? CitizenIdentification { get; set; } = string.Empty;
        public DateTime? CitizenIdentificationDate { get; set; }
        public string? Major { get; set; } = string.Empty;
        public bool? Internable { get; set; }
        public bool? FullTime { get; set; }
        public string? Cvfile { get; set; } = string.Empty;
        public int? InternSpecialized { get; set; }
        public string? TelephoneNum { get; set; } = string.Empty;
        public string? InternStatus { get; set; } = string.Empty;       
        public DateTime? RegisteredDate { get; set; }
        public string? HowToKnowAlta { get; set; } = string.Empty;  
        public string? InternPassword { get; set; } = string.Empty;
        public string? ForeignLanguage { get; set; } = string.Empty;
        public short? YearOfExperiences { get; set; }
        public bool? PasswordStatus { get; set; }
        public bool? ReadyToWork { get; set; }
        public bool? InternEnabled { get; set; }
        public float? EntranceTest { get; set; }
        public string? Introduction { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public string? LinkProduct { get; set; } = string.Empty;
        public string? JobFields { get; set; } = string.Empty;      
        public bool? HiddenToEnterprise { get; set; }

    }
}
