using BTBackendOnline2.Configurations;
using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace BTBackendOnline2.Services.Implements
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ApiResponse<UserRes> Create(UserReq? request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponse<UserRes>.Fail("Request is null");
                }

                User user = new()
                {
                    RoleId = request.RoleId,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Address = request.Address
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                UserRes response = new()
                {
                    UserId = user.UserId,
                    RoleId = request.RoleId,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Address = request.Address
                };

                return ApiResponse<UserRes>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Create method");
                return ApiResponse<UserRes>.Fail($"Error: {ex.Message}");
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var existing = _context.Users.FirstOrDefault(r => r.UserId == id);
                if (existing == null)
                {
                    return ApiResponse<bool>.Fail("User is not found");
                }

                _context.Users.Remove(existing);
                _context.SaveChanges();
                return ApiResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Delete method");
                return ApiResponse<bool>.Fail($"Error: {ex.Message}");
            }
        }

        public ApiResponse<List<UserRes>> GetAll()
        {
            try
            {
                var users = _context.Users
                    .Select(r => new UserRes
                    {
                        UserId = r.UserId,
                        RoleId = r.RoleId,
                        FullName = r.FullName,
                        BirthDate = r.BirthDate,
                        Address = r.Address
                    })
                    .ToList();

                return ApiResponse<List<UserRes>>.Success(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAll method");
                return ApiResponse<List<UserRes>>.Fail($"Error: {ex.Message}");
            }
        }

        public ApiResponse<UserRes> Update(UserReq request, int id)
        {
            try
            {
                var existing = _context.Users.FirstOrDefault(r => r.UserId == id);
                if (existing == null)
                {
                    return ApiResponse<UserRes>.Fail("User is not found");
                }

                existing.RoleId = request.RoleId;
                existing.FullName = request.FullName;
                existing.BirthDate = request.BirthDate;
                existing.Address = request.Address;

                UserRes response = new()
                {
                    UserId = id,
                    RoleId = request.RoleId,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Address = request.Address
                };

                _context.Users.Update(existing);
                _context.SaveChanges();
                return ApiResponse<UserRes>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Update method");
                return ApiResponse<UserRes>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
