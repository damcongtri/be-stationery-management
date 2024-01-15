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
        public async Task<IActionResult> Post([FromForm] StationeryDto s)
        {
          
                var stationery = new StationeryDto { StationeryId=s.StationeryId,Name=s.Name,Description=s.Description,Price=s.Price,Inventory=s.Inventory,ReorderLevel=s.ReorderLevel,SupplierId=s.SupplierId,UnitPrice=s.UnitPrice };
                //xử lý ảnh
                if (s.ImageFile.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "images", Path.GetRandomFileName());
                    using (var stream = System.IO.File.Create(path))
                    {
                        await s.ImageFile.CopyToAsync(stream);
                    }
                    stationery.Image = "/images/" + path;
                }
                else
                {
                stationery.Image = "";
                }
            _stationeryservice.CreateStationery( stationery);
               
                return Ok(stationery);
          
        }

        // PUT api/<StationeryController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int itemid, [FromForm] StationeryDto s)
        {
            var stationery = new StationeryDto { StationeryId = s.StationeryId, Name = s.Name, Description = s.Description, Price = s.Price, Inventory = s.Inventory, ReorderLevel = s.ReorderLevel, SupplierId = s.SupplierId, UnitPrice = s.UnitPrice };
            if (s.ImageFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", Path.GetRandomFileName());
                using (var stream = System.IO.File.Create(path))
                {
                    await s.ImageFile.CopyToAsync(stream);
                }
                stationery.Image = "/images/" + path;
                if (!string.IsNullOrEmpty(stationery.Image))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), stationery.Image.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }
            else
            {
                stationery.Image = "";
            }
            _stationeryservice.UpdateStationery(stationery,itemid);

            return Ok(stationery);
        }

        // DELETE api/<StationeryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int itemid)
        {
            return Ok(await _stationeryservice.DeleteStationery(itemid));
        }
        
    }
}
