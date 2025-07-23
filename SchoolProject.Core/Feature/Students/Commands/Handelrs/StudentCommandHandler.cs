using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Resources;
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
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion

    #region Constructor
    public StudentCommandHandler(IStudentService studentService, IMapper mapper,
        IStringLocalizer<SharedResources> localizer)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localizer = localizer;
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
        var existingStudent = await _studentService.GetStudentById_WithoutDepartmentDetails_Async(request.Id);

        if (existingStudent == null)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = _localizer[SharedResourcesKeys.NotFoundStudent],
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

        _mapper.Map(request, existingStudent);

        await _studentService.EditStudentAsync(existingStudent);
        return new ApiResponse<string>
        {
            IsSuccess = true,
            Message = _localizer[SharedResourcesKeys.EditStudent],
            StatusCode = System.Net.HttpStatusCode.NotFound
        };

    }
    public async Task<ApiResponse<string>> Handle(EditStudentDepartmentCommand request, CancellationToken cancellationToken)
    {
        var existingStudent = await _studentService.GetStudentByIdAsync(request.Id);
        if (existingStudent == null)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = _localizer[SharedResourcesKeys.NotFoundStudent],
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
        _mapper.Map(request, existingStudent);

        await _studentService.EditStudentDepartmentAsync(existingStudent);
        return new ApiResponse<string>
        {
            IsSuccess = false,
            Message = _localizer[SharedResourcesKeys.EditStudentDepartment],
            StatusCode = System.Net.HttpStatusCode.NotFound
        };
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
                    Message = _localizer[SharedResourcesKeys.NotFoundStudent],
                };
            }
            var result = await _studentService.DeleteStudentAsync(existingStudent);

            if (result == "Success")
            {
                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = _localizer[SharedResourcesKeys.DeletedStudent],
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = _localizer[SharedResourcesKeys.DeletedFailedStudent],
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