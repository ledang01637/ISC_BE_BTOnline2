using BTBackendOnline2.Configurations;
using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BTBackendOnline2.Services.Implements
{
    public class AllowAccessService : IAllowAccess
    {
        private readonly AppDbContext _context;

        public AllowAccessService(AppDbContext context)
        {
            _context = context;
        }

        public ApiResponse<AllowAccessRes> Create(AllowAccessReq? request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponse<AllowAccessRes>.Fail("Request is null");
                }

                AllowAccess allowAccess = new()
                {
                    TableName = request.TableName,
                    RoleId = request.RoleId,
                    AccessPropertiesJson = request.AccessPropertiesJson,
                };

                _context.AllowAccess.Add(allowAccess);
                _context.SaveChanges();

                var response = new AllowAccessRes
                {
                    Id = allowAccess.Id,
                    RoleId = allowAccess.RoleId,
                    TableName = allowAccess.TableName,
                    AccessPropertiesJson = allowAccess.AccessPropertiesJson
                };

                return ApiResponse<AllowAccessRes>.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<AllowAccessRes>.Fail($"An error occurred while creating AllowAccess: {ex.Message}");
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var existing = _context.AllowAccess.FirstOrDefault(r => r.Id == id);
                if (existing == null)
                {
                    return ApiResponse<bool>.Fail("AllowAccess is not found");
                }

                _context.AllowAccess.Remove(existing);
                _context.SaveChanges();
                return ApiResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"An error occurred while deleting AllowAccess: {ex.Message}");
            }
        }

        public ApiResponse<List<AllowAccessRes>> GetAll()
        {
            try
            {
                var allowAccessList = _context.AllowAccess
                    .Select(r => new AllowAccessRes
                    {
                        Id = r.Id,
                        TableName = r.TableName,
                        RoleId = r.RoleId,
                        AccessPropertiesJson = r.AccessPropertiesJson.Trim('[', ']').Replace("\"", "")
                    })
                    .ToList();



                return ApiResponse<List<AllowAccessRes>>.Success(allowAccessList);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<AllowAccessRes>>.Fail($"An error occurred while retrieving AllowAccess list: {ex.Message}");
            }
        }

        public ApiResponse<AllowAccessRes> Update(AllowAccessReq request, int id)
        {
            try
            {
                var existing = _context.AllowAccess.FirstOrDefault(r => r.Id == id);
                if (existing == null)
                {
                    return ApiResponse<AllowAccessRes>.Fail("AllowAccess is not found");
                }

                existing.TableName = request.TableName;
                existing.RoleId = request.RoleId;
                existing.AccessPropertiesJson = request.AccessPropertiesJson;

                AllowAccessRes response = new()
                {
                    Id = id,
                    RoleId = request.RoleId,
                    TableName = request.TableName,
                    AccessPropertiesJson = request.AccessPropertiesJson
                };

                _context.AllowAccess.Update(existing);
                _context.SaveChanges();
                return ApiResponse<AllowAccessRes>.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<AllowAccessRes>.Fail($"An error occurred while updating AllowAccess: {ex.Message}");
            }
        }
    }
}
