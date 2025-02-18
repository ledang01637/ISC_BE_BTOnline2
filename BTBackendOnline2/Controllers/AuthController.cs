using BTBackendOnline2.DB;
using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BTBackendOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogin _service;

        public AuthController(ILogin service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginReq request)
        {
            var response = _service.AuthLogin(request);

            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        
    }
}
