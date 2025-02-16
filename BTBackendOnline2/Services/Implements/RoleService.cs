using BTBackendOnline2.Configurations;
using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using System;

namespace BTBackendOnline2.Services.Implements
{
    public class RoleService : IRole
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public ApiResponse<RoleRes> Create(RoleReq? request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponse<RoleRes>.Fail("Request is null");
                }

                Role role = new()
                {
                    RoleName = request.RoleName
                };
                _context.Roles.Add(role);
                _context.SaveChanges();

                RoleRes response = new()
                {
                    RoleId = role.RoleId,
                    RoleName = request.RoleName
                };

                return ApiResponse<RoleRes>.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<RoleRes>.Fail($"An error occurred while creating the role: {ex.Message}");
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var existing = _context.Roles.FirstOrDefault(r => r.RoleId == id);
                if (existing == null)
                {
                    return ApiResponse<bool>.Fail("Role not found");
                }

                _context.Roles.Remove(existing);
                _context.SaveChanges();
                return ApiResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"An error occurred while deleting the role: {ex.Message}");
            }
        }

        public ApiResponse<List<RoleRes>> GetAll()
        {
            try
            {
                var roles = _context.Roles
                    .Select(r => new RoleRes
                    {
                        RoleId = r.RoleId,
                        RoleName = r.RoleName
                    })
                    .ToList();

                return ApiResponse<List<RoleRes>>.Success(roles);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<RoleRes>>.Fail($"An error occurred while retrieving roles: {ex.Message}");
            }
        }

        public ApiResponse<RoleRes> Update(RoleReq request, int id)
        {
            try
            {
                var existing = _context.Roles.FirstOrDefault(r => r.RoleId == id);
                if (existing == null)
                {
                    return ApiResponse<RoleRes>.Fail("Role not found");
                }

                existing.RoleName = request.RoleName;

                RoleRes response = new()
                {
                    RoleId = id,
                    RoleName = request.RoleName,
                };

                _context.Roles.Update(existing);
                _context.SaveChanges();
                return ApiResponse<RoleRes>.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<RoleRes>.Fail($"An error occurred while updating the role: {ex.Message}");
            }
        }
    }
}
