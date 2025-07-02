using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Authorization.Commands.Models;

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
    }
}
