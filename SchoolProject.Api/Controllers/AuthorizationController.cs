using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("AddRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(new { message = result.Message });
            return BadRequest(new { message = result.Message });
        }
        [HttpGet("RolesList")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);

        }
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRole([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));
            return Ok(result);
        }
        [HttpGet("ManageUserRoles/{userId}")]
        public async Task<IActionResult> ManageUserRoles([FromRoute] string userId)
        {
            var response = await _mediator.Send(new ManageUserRolesQuery(userId));
            return Ok(response);
        }
        [HttpPut("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
