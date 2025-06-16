using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Department.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
