using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddAppUserCommand user)
        {
            var result = await _mediator.Send(user);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
        [HttpGet("UsersPaginatedList")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersPaginatedListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetByUserName/{username}")]
        public async Task<IActionResult> GetSingleUser([FromRoute] string username)
        {
            var result = await _mediator.Send(new GetSingleUserByUserNameQuery(username));
            return Ok(result);
        }
    }
}
