using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = " ادارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpGet("ManageUserRoles/{userId}")]
        public async Task<IActionResult> ManageUserRoles([FromRoute] string userId)
        {
            var response = await _mediator.Send(new ManageUserRolesQuery(userId));
            return Ok(response);
        }
    }
}
