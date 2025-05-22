using System.Net;
using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

public class StudentCommandHandler : IRequestHandler<AddStudentCommand, ApiResponse<string>>,
                                     IRequestHandler<EditStudentCommand, ApiResponse<string>>,
                                     IRequestHandler<EditStudentDepartmentCommand, ApiResponse<string>>,
                                     IRequestHandler<DeleteStudentCommand, ApiResponse<string>>

{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public StudentCommandHandler(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    #endregion

    #region Handelrs Methods
    public async Task<ApiResponse<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var mappedStudent = _mapper.Map<Student>(request);
        return await _studentService.AddStudentAsync(mappedStudent);
    }
    public async Task<ApiResponse<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        var existingStudent = await _studentService.GetStudentByIdAsync(request.Id);

        if (existingStudent == null)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = "Student not found.",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

        _mapper.Map(request, existingStudent);

        return await _studentService.EditStudentAsync(existingStudent);
    }
    public async Task<ApiResponse<string>> Handle(EditStudentDepartmentCommand request, CancellationToken cancellationToken)
    {
        var existingStudent = await _studentService.GetStudentByIdAsync(request.Id);
        if (existingStudent == null)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = "Student not found.",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
        _mapper.Map(request, existingStudent);

        return await _studentService.EditStudentDepartmentAsync(existingStudent);
    }
    public async Task<ApiResponse<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingStudent = await _studentService.GetStudentById_WithoutDepartmentDetails_Async(request.Id);
            if (existingStudent == null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "Student not found."
                };
            }
            var result = await _studentService.DeleteStudentAsync(existingStudent);

            if (result == "Success")
            {
                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = "Student deleted successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = result,
                // Message = "Failed to delete student.",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,

                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }

    }
    #endregion
}