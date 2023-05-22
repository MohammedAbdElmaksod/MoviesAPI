using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Dtos;
using test1.Models;
using test1.Services;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenrasController : ControllerBase
    {
        private readonly IGenraService _genraService;

        public GenrasController(IGenraService genraService)
        {
            _genraService = genraService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _genraService.GetAllGenraAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenraAsync(GenraCreatorDto dto)
        {
            var genra = new Genra { Name = dto.name };
            await _genraService.CreateGenraAsync(genra);
            return Ok(genra);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]GenraCreatorDto dto)
        {
            var genra = await _genraService.GetGenraByIdAsync(id);
            if(genra is null)
                return NotFound($"No Genra found with id : {id}");
            genra.Name= dto.name;

            _genraService.UpdateGenra(genra);
            return Ok(genra);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(int id)
        {
            var genra = await _genraService.GetGenraByIdAsync(id);
            if (genra is null)
                return NotFound($"No Genra found with id : {id}");

            _genraService.DeleteGenra(genra);
            return Ok($"this genra : {{{genra.Name}}}is deleted");
        }
    }
}
