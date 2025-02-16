namespace BTBackendOnline2.DTOs.Request
{
    public class UserReq
    {
        public int RoleId { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
    }
}
