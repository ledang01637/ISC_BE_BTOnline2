using BTBackendOnline2.Configurations;
using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BTBackendOnline2.Services.Implements
{
    public class LoginService : ILogin
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public LoginService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public ApiResponse<LoginRes> AuthLogin(LoginReq request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == ComputeSha256(request.Password));

                if (user == null)
                {
                    return ApiResponse<LoginRes>.Fail("UnAuthenticated");
                }

                var token = GenerateTokens(user);

                if (string.IsNullOrEmpty(token.Item1))
                {
                    return ApiResponse<LoginRes>.Fail("Toke is null");
                }

                return ApiResponse<LoginRes>.Success(new LoginRes
                {
                    AccessToken = token.Item1,
                    RefreshTokens = token.Item2
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginRes>.Fail($"Error: {ex.Message}");
            }

        }

        public (string, RefreshToken) GenerateTokens(User user,
                                                            int accessExpire = 15,
                                                            int refreshExpire = 600)
        {

            var role = _context.Roles.FirstOrDefault(a => a.RoleId == user.RoleId);

            if (role == null || string.IsNullOrEmpty(role.RoleName))
            {
                throw new Exception("User role is not valid");
            }

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                throw new Exception("User is not valid");
            }

            var jwt = _configuration.GetSection("Jwt").Get<TokenRequiment>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role.RoleName),
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(accessExpire),
                    signingCredentials: signIn
                    );
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            Console.WriteLine(accessToken);

            RefreshToken refreshToken = new()
            {
                Token = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.UtcNow.AddMinutes(refreshExpire),
                Email = user.Email
            };
            return (accessToken, refreshToken);
        }

        public (string?, RefreshToken?) GenerateTokens(User user,
                                                            RefreshToken comparedToken,
                                                            int accessExpire = 15,
                                                            int refreshExpire = 600)
        {
            if (comparedToken == null || comparedToken.ExpireDate < DateTime.UtcNow)
            {
                return (null, null);
            }

            var role = _context.Roles.FirstOrDefault(a => a.RoleId == user.RoleId);

            if (role == null || string.IsNullOrEmpty(role.RoleName))
            {
                throw new Exception("User role is not valid");
            }

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                throw new Exception("User is not valid");
            }

            var jwt = _configuration.GetSection("Jwt").Get<TokenRequiment>();

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString()),
                 new Claim(ClaimTypes.Name, user.Email),
                 new Claim(ClaimTypes.Role, role.RoleName),
                 new Claim("RoleId", user.RoleId.ToString()),
                 new Claim("Email", user.Email) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(accessExpire),
                signingCredentials: signIn
             );
            string accessTokens = new JwtSecurityTokenHandler().WriteToken(token);

            RefreshToken refreshToken = new()
            {
                Token = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.UtcNow.AddMinutes(refreshExpire),
                Email = user.Email
            };
            return (accessTokens, refreshToken);
        }

        public static string ComputeSha256(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input + "ledang"));
            StringBuilder builder = new();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

    }
}
