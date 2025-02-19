using BTBackendOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTBackendOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InternController : ControllerBase
    {
        private readonly IIntern _service;
        public InternController(IIntern service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var roleId = User.FindFirst("RoleId")?.Value;

            if(string.IsNullOrEmpty(roleId) )
            {
                return BadRequest();
            }

            var response = _service.GetAll(int.Parse(roleId));

            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }
    }
}
