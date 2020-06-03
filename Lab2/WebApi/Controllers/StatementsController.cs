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
    public class StatementsController : Controller
    {
        private readonly IStatementService _statementService;
        private readonly ILogger<StatementsController> _logger;

        public StatementsController(IStatementService statementService, ILogger<StatementsController> logger)
        {
            _statementService = statementService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _statementService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _statementService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Statement statement)
        {
            try
            {
                await _statementService.Create(statement);
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
                await _statementService.Delete(id);
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
        public async Task<IActionResult> Update([FromBody] Statement statement)
        {
            try
            {
                await _statementService.Update(statement);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStatementsByStudentId")]
        public async Task<IActionResult> GetStatementsByStudentId(int id)
        {
            var specialty = await _statementService.GetStatementsByStudentId(id);

            return Ok(specialty);
        }

        [HttpGet("GetStatementsBySubjectId")]
        public async Task<IActionResult> GetStatementsBySubjectId(int id)
        {
            var specialty = await _statementService.GetStatementsByStudentId(id);

            return Ok(specialty);
        }

        [HttpGet("GetStatementsBySemesterId")]
        public async Task<IActionResult> GetStatementsBySemestertId(int id)
        {
            var specialty = await _statementService.GetStatementsByStudentId(id);

            return Ok(specialty);
        }
    }
}
