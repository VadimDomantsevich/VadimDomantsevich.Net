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
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<GroupsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<GroupsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var result = await _studentService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _studentService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            try
            {
                await _studentService.Create(student);
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
                await _studentService.Delete(id);
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
        public async Task<IActionResult> Update([FromBody] Student student)
        {
            try
            {
                await _studentService.Update(student);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentsByGroupId")]
        public async Task<IActionResult> GetStudentsByGroupId(int id)
        {
            var students = await _studentService.GetStudentsByGroupId(id);

            return Ok(students);
        }

        [HttpGet("GetStudentByRecordNumber")]
        public async Task<IActionResult> GetStudentByRecordNumber(string number)
        {
            var student = await _studentService.GetStudentByRecordNumber(number);

            return Ok(student);
        }

        [HttpGet("GetStudentByPhoneNumber")]
        public async Task<IActionResult> GetStudentByPhoneNumber(string number)
        {
            var student = await _studentService.GetStudentByPhoneNumber(number);

            return Ok(student);
        }
    }
}
