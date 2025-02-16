using BTBackendOnline2.Configurations;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;

namespace BTBackendOnline2.Services.Interfaces
{
    public interface IAllowAccess
    {
        /// <summary>
        /// Lấy danh sách AllowAccess
        /// </summary>
        /// <returns></returns>
        ApiResponse<List<AllowAccessRes>> GetAll();

        /// <summary>
        /// Thêm AllowAccess
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResponse<AllowAccessRes> Create(AllowAccessReq request);

        /// <summary>
        /// Sửa AllowAccess
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<AllowAccessRes> Update(AllowAccessReq request, int id);

        /// <summary>
        /// Xóa AllowAccess
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<bool> Delete(int id);
    }
}
