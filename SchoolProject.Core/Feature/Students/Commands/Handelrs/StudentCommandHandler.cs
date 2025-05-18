using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

public class AddStudentHandler : IRequestHandler<AddStudentCommand, ApiResponse<string>>
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public AddStudentHandler(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var mappedStudent = _mapper.Map<Student>(request);
        return await _studentService.AddStudentAsync(mappedStudent);
    }
}
