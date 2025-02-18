using BTBackendOnline2.Configurations;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;

namespace BTBackendOnline2.Services.Interfaces
{
    public interface ILogin
    {
        /// <summary>
        /// Tạo token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResponse<LoginRes> AuthLogin(LoginReq request);
    }
}
