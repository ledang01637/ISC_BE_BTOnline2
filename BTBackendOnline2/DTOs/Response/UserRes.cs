namespace BTBackendOnline2.DTOs.Response
{
    public class UserRes
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
    }
}
