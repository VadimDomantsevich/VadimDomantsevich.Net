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
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupService groupService, ILogger<GroupsController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _groupService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _groupService.GetById(id);
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
        public async Task<IActionResult> Add([FromBody] Group group)
        {
            try
            {
                await _groupService.Create(group);
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
                await _groupService.Delete(id);
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
        public async Task<IActionResult> Update([FromBody] Group group)
        {
            try
            {
                await _groupService.Update(group);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGroupsBySpecialtyId")]
        public async Task<IActionResult> GetGroupsBySpecialtyId(int groupId)
        {
            var groups = await _groupService.GetGroupsBySpecialtyId(groupId);

            return Ok(groups);
        }

        [HttpGet("GetGroupByName")]
        public async Task<IActionResult> GetGroupByName(string name)
        {
            var group = await _groupService.GetGroupByName(name);

            return Ok(group);
        }
    }
}
