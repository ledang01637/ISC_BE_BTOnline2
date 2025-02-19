using BTBackendOnline2.Configurations;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;

namespace BTBackendOnline2.Services.Interfaces
{
    public interface IIntern
    {
        /// <summary>
        /// Lấy danh sách Intern
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        ApiResponse<List<Dictionary<string, object>>> GetAll(int roleId);
    }
}
