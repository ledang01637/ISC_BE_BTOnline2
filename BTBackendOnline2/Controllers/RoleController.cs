using BTBackendOnline2.DTOs.Request;
using BTBackendOnline2.Models;
using BTBackendOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTBackendOnline2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _service;
        public RoleController(IRole service ) 
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
        public IActionResult CreateRole([FromBody] RoleReq roleRequest)
        {
            var response = _service.Create(roleRequest);

            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleReq roleRequest)
        {
            var response = _service.Update(roleRequest, id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var response = _service.Delete(id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

    }
}
