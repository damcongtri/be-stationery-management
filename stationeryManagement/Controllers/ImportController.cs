using Microsoft.AspNetCore.Mvc;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service;
using stationeryManagement.Service.Interface;
using System.Composition;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stationeryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private IImportService _importService;
        public ImportController(IImportService importService)
        {
            _importService = importService;
        }
        // GET: api/<ImportController>
        [HttpGet]
        public async Task<IEnumerable<Import>> Get()
        {
            return await _importService.GetAllImport();
        }

        // GET api/<ImportController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ImportId)
        {
            return Ok(await _importService.GetImportId(ImportId));
        }
        
        // POST api/<ImportController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImportDto import)
        {
            var result = await _importService.CreateImport(import ,import.UserCreateId);
            return Ok(result);
        }

        // PUT api/<ImportController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put( [FromBody] ImportDto import,int id)
        {
            var result = await _importService.UpdateImport(import, import.UserCreateId, id);
            return Ok(result);
        }

        // DELETE api/<ImportController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ImportId)

        {
            return Ok(await _importService.DeleteImport(ImportId));
        }
    }
}
