using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;
using SchoolProject.Core.Feature.AuthenticationUser.Queries.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;

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
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
        [HttpGet("ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return Conflict(result);
            return Ok(result);
        }
        [HttpPost("send-reset-password-otp")]
        public async Task<IActionResult> SendResetPasswordOtp([FromBody] SendResetPasswordOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("confirm-otp")]
        public async Task<IActionResult> ConfirmOtp([FromBody] VerifyOTPCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
