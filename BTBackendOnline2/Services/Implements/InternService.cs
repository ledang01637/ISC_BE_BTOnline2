using BTBackendOnline2.Configurations;
using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs.Response;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;

namespace BTBackendOnline2.Services.Implements
{
    public class InternService : IIntern
    {
        private readonly AppDbContext _context;

        public InternService(AppDbContext context)
        {
            _context = context;
        }
        public ApiResponse<List<Dictionary<string, object>>> GetAll(int roleId)
        {
            var allColumns = typeof(Intern).GetProperties()
                                              .Where(p => p.Name != "Id")
                                              .ToDictionary(p => p.Name, p => p);

            var allowedColumns = _context.AllowAccess
                                .Where(a => a.RoleId == roleId && a.TableName == "Intern")
                                .Select(a => a.AccessPropertiesJson)
                                .FirstOrDefault();

            if (!string.IsNullOrEmpty(allowedColumns))
            {
                allowedColumns = allowedColumns.Trim('[', ']').Replace("\"", "");
            }

            var allowedColumnsList = allowedColumns?.Split(',').Select(c => c.Trim()).ToList() ?? new List<string>();

            var query = _context.Interns
                        .AsEnumerable()
                        .Select(intern => allowedColumnsList.ToDictionary(
                            column => column,
                            column => allColumns.TryGetValue(column, out var col) ? col.GetValue(intern) : null
                        ))
                        .ToList();


            return ApiResponse<List<Dictionary<string, object>>>.Success(query);
        }

    }
}
