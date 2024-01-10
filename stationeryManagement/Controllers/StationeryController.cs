using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service;
using stationeryManagement.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationeryController : ControllerBase
    {
        private IStationeryService _stationeryservice;
        public StationeryController(IStationeryService stationeryservice)
        {
            _stationeryservice = stationeryservice;
        }
        // GET: api/<StationeryController>
        [HttpGet]
        public async Task<IEnumerable<Stationery>> Get()
        {
            return await _stationeryservice.GetAllStationery();
        }

        // GET api/<StationeryController>/5
        [HttpGet("{id}", Name = "stationery")]
        public async Task<IActionResult> Get(int itemid)
        {
            return Ok(await _stationeryservice.GetStationeryById(itemid));
        }

        // POST api/<StationeryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StationeryDto stationery)
        {
            var result = await _stationeryservice.CreateStationery(stationery);
            return Ok(result);
        }

        // PUT api/<StationeryController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int itemid, [FromBody] StationeryDto stationery)
        {
            var result = await _stationeryservice.UpdateStationery(stationery, itemid);
            return Ok(result);
        }

        // DELETE api/<StationeryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int itemid)
        {
            return Ok(await _stationeryservice.DeleteStationery(itemid));
        }
        
    }
}
