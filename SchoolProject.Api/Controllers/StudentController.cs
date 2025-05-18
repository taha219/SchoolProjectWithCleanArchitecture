using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Feature.Students.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(result);
        }
        [HttpPost("AddStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand std)
        {
            var result = await _mediator.Send(std);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
    }
}
