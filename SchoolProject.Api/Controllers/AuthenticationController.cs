using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
    }
}
