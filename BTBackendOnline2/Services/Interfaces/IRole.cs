using BTBackendOnline2.Configurations;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;

namespace BTBackendOnline2.Services.Interfaces
{
    public interface IRole
    {
        /// <summary>
        /// Lấy danh sách Role
        /// </summary>
        /// <returns></returns>
        ApiResponse<List<RoleRes>> GetAll();

        /// <summary>
        /// Thêm Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResponse<RoleRes> Create(RoleReq request);

        /// <summary>
        /// Sửa Role
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<RoleRes> Update(RoleReq request, int id);

        /// <summary>
        /// Xóa Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResponse<bool> Delete(int id);


    }
}
