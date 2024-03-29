using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.RoleDto;
using stationeryManagement.Service;
using stationeryManagement.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
          private IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _roleService.GetAllRole();
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}", Name = "role")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _roleService.GetRoleById(id));
        }

        // POST api/<RoleController>
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleDto role)
        {
            var result = await _roleService.CreateRole(role);
            return Ok(result);
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RoleDto role)
        {
            var result = await _roleService.UpdateRole(role, id);
            return Ok(result);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _roleService.DeleteRole(id));
        }
    }
}
