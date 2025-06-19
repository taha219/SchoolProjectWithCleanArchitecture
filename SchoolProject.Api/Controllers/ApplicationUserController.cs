using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
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
        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return Conflict(result);
            return Ok(result);
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeleteUserCommman(id));
            if (!result.IsSuccess)
                return Conflict(result);
            return Ok(result);
        }
    }
}
