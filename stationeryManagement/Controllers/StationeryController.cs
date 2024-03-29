using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.StationeryDto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationeryController : ControllerBase
    {
        private readonly IStationeryService _stationeryService;

        public StationeryController(IStationeryService stationeryService)
        {
            _stationeryService = stationeryService;
        }

        // GET: api/<StationeryController>
        [HttpGet]
        public async Task<IEnumerable<Stationery>> Get()
        {
            return await _stationeryService.GetAllStationery();
        }

        // GET api/<StationeryController>/5
        [HttpGet("{id:int}", Name = "stationery")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _stationeryService.GetStationeryById(id));
        }

        // POST api/<StationeryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] StationeryDto stationeryDto)
        {
            var result = await _stationeryService.CreateStationery(stationeryDto);
            return Ok(result);
        }

        // PUT api/<StationeryController>/5
        [HttpPut("{stationeryId:int}")]
        public async Task<IActionResult> Put([FromForm] StationeryDto stationeryDto,int stationeryId)
        {
            var result = await _stationeryService.UpdateStationery(stationeryDto, stationeryId);
            return Ok(result);
        }

        // DELETE api/<StationeryController>/5
        [HttpDelete("{stationeryId:int}")]
        public async Task<IActionResult> Delete(int stationeryId)
        {
            return Ok(await _stationeryService.DeleteStationery(stationeryId));
        }
    }
}