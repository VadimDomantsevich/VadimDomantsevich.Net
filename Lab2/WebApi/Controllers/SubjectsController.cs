using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<GroupsController> _logger;

        public SubjectsController(ISubjectService subjectService, ILogger<GroupsController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAll()
        {
            var result = await _subjectService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _subjectService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Subject subject)
        {
            try
            {
                await _subjectService.Create(subject);
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
                await _subjectService.Delete(id);
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
        public async Task<IActionResult> Update([FromBody] Subject subject)
        {
            try
            {
                await _subjectService.Update(subject);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSubjectByName")]
        public async Task<IActionResult> GetSubjectByName(string name)
        {
            var specialty = await _subjectService.GetSubjectByName(name);

            return Ok(specialty);
        }
    }
}
