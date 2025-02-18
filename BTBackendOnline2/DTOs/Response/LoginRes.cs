using BTBackendOnline2.Models;

namespace BTBackendOnline2.DTOs.Response
{
    public class LoginRes
    {
        public string? AccessToken {  get; set; }
        public RefreshToken? RefreshTokens { get; set; }
    }
}
