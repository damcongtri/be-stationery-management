using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data.Dto.RequestDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Static;

namespace stationeryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/Request
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Request/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Request
        [HttpPost("create")]
        public void Create([FromBody] RequestCreateDto requestCreateDto)
        {
        } // POST: api/Request
        [Authorize(Roles = Roles.Manager)]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmRequest([FromBody] int requestId, int status)
        {
            return  BadRequest("aa");
        }
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] int requestId)
        {
            return  BadRequest("aa");
        }
        
    }
}
