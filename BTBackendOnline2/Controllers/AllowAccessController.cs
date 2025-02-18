using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTBackendOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AllowAccessController : ControllerBase
    {
        private readonly IAllowAccess _service;
        public AllowAccessController(IAllowAccess service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult GetList()
        {
            var response = _service.GetAll();

            return response.Code == 0 ? Ok(response) : response.Code == 2 ? Unauthorized("Unauthorized") : BadRequest(response);
        }

        [HttpPost]
        public IActionResult CreateAllowAccess([FromBody] AllowAccessReq request)
        {
            var response = _service.Create(request);

            return response.Code == 0 ? Ok(response) : response.Code == 2 ? Unauthorized() : BadRequest(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAllowAccess(int id, [FromBody] AllowAccessReq request)
        {
            var response = _service.Update(request, id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAllowAccess(int id)
        {
            var response = _service.Delete(id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }
    }
}
