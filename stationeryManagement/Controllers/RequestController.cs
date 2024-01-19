using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data.Dto.RequestDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Static;
using stationeryManagement.Service.Exceptions;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/Request
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Request>> Get()
        {
            return await _requestService.GetAllRequest();
        }

        // GET: api/Request/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _requestService.GetRequestById(id));
        }
        [HttpGet("GetMineRequest")]
        public async Task<IActionResult> GetForUser()
        {
            var userIdClaim = User.FindFirst(claim => claim.Type == ClaimTypes.Sid);
            var userId = Guid.Parse(userIdClaim.Value) ;
            return Ok(await _requestService.GetRequestForUser(userId));
        }

        // POST: api/Request
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RequestCreateDto requestCreateDto)
        {
            var userIdClaim = User.FindFirst(claim => claim.Type == ClaimTypes.Sid);
            var userId = Guid.Parse(userIdClaim.Value) ;
            if (!(userId is Guid) )
            {
                return BadRequest("không tìm thấy user");
            }
            return Ok(await _requestService.CreateRequest(requestCreateDto,userId));
        } 
        // POST: api/Request
        [Authorize(Roles = $"{Roles.Manager}, {Roles.Admin}")]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmRequest(RequestConfirmDto requestCreateDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(claim => claim.Type == ClaimTypes.Sid);
                var userId = Guid.Parse(userIdClaim.Value);
                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    throw new BadRequestException("Có lỗi trong khi đăng nhập ");
                }
                return Ok(await _requestService.UpdateStatus(requestCreateDto.RequestId, requestCreateDto.Status,
                    userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel(RequestConfirmDto requestCreateDto)
        {
            var userIdClaim = User.FindFirst(claim => claim.Type == ClaimTypes.Sid);
            var userId = Guid.Parse(userIdClaim.Value) ;
            return Ok(await _requestService.UpdateStatus(requestCreateDto.RequestId,RequestStatus.Cancel,userId));
        }
        
    }
}
