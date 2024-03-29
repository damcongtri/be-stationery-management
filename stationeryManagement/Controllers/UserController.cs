using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.UserDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Static;
using stationeryManagement.Service.Exceptions;
using stationeryManagement.Service.Interface;
using stationeryManagement.Service.Utils;

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _userService.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        // GET: api/User/5
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            if (user is null)
            {
                return NotFound("User không tồn tại");
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UserDto userDto)
        {
            var user = await _userService.CreateUser(userDto);
            if (user is null)
            {
                return BadRequest("có lỗi xảy ra");
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] UserDto userDto)
        {
            return Ok(await _userService.UpdateUser(userDto, id));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}