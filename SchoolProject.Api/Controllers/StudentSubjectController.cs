using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.StudentsSubjects.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentSubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Degree")]
        public async Task<IActionResult> AddDegree([FromBody] AddStudentGradeCommand std)
        {
            var result = await _mediator.Send(std);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }

        [HttpPut("Editdegrees")]
        public async Task<IActionResult> EditStudentdegree([FromBody] UpdateStudentGradeCommand std)
        {
            var result = await _mediator.Send(std);

            if (!result.IsSuccess)
                return Conflict(result);

            return Ok(result);
        }
    }
}
