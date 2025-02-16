using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTBackendOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _service;
        public UserController(IUser service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var response = _service.GetAll();

            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserReq userRequest)
        {
            var response = _service.Create(userRequest);

            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserReq userRequest)
        {
            var response = _service.Update(userRequest, id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var response = _service.Delete(id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }
    }
}
