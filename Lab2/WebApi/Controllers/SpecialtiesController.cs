using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly ILogger<GroupsController> _logger;

        public SpecialtiesController(ISpecialtyService specialtyService, ILogger<GroupsController> logger)
        {
            _specialtyService = specialtyService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _specialtyService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _specialtyService.GetById(id);
                _logger.LogInformation("Searched for Group");

                return Ok(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Specialty specialty)
        {
            try
            {
                await _specialtyService.Create(specialty);
                _logger.LogInformation("Added new group");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _specialtyService.Delete(id);
                _logger.LogInformation("Deleted group");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Specialty specialty)
        {
            try
            {
                await _specialtyService.Update(specialty);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSpecialtyByName")]
        public async Task<IActionResult> GetSpecialtyByName(string name)
        {
            var specialty = await _specialtyService.GetSpecialtyByName(name);

            return Ok(specialty);
        }
    }
}
