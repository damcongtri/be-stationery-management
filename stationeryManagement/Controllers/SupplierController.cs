using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.SupplierDto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service;
using stationeryManagement.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService=supplierService;
        }
        // GET: api/<SupplierController>
        [HttpGet]
        public async Task<IEnumerable<Supplier>> Get()
        {
            return await _supplierService.GetAllSupplier();
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}", Name = "supplier")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _supplierService.GetSupplierById(id));
        }

        // POST api/<SupplierController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierDto supplier)
        {
            var result = await _supplierService.CreateSupplier(supplier);
            return Ok(result);
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> Put(int id, [FromBody] SupplierDto supplier)
        {
            var result = await _supplierService.UpdateSupplier(supplier, id);
            return  Ok(result);
        }

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _supplierService.DeleteSupplier(id));
        }
    }
}
