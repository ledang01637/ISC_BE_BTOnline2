using BTBackendOnline2.Configurations;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;

namespace BTBackendOnline2.Services.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Lấy danh sách User
        /// </summary>
        /// <returns></returns>
        ApiResponse<List<UserRes>> GetAll();

        /// <summary>
        /// Thêm User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResponse<UserRes> Create(UserReq request);

        /// <summary>
        /// Sửa User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<UserRes> Update(UserReq request, int id);

        /// <summary>
        /// Xóa User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<bool> Delete(int id);
    }
}
