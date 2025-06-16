using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Department.Queries.Models;
using SchoolProject.Core.Feature.Department.Queries.ResponesModels;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Services.Abstract;
using static SchoolProject.Core.Feature.Department.Queries.ResponesModels.GetDepartmentByIdResponseModel;

namespace SchoolProject.Core.Feature.Department.Queries.Handelrs
{
    public class DepartmentQueryHandler : IRequestHandler<GetDepartmentByIdQuery, ApiResponse<GetDepartmentByIdResponseModel>>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;

        public DepartmentQueryHandler(IStudentService studentService, IDepartmentService departmentService, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper)
        {
            _studentService = studentService; ;
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetDepartmentByIdResponseModel>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null)
            {
                return new ApiResponse<GetDepartmentByIdResponseModel>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.NotFoundDepartment]
                };
            }
            var mappedDepartment = _mapper.Map<GetDepartmentByIdResponseModel>(department);

            Expression<Func<SchoolProject.Data.Entities.Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.localize(e.NameAr, e.NameEn));
            var studentQueryable = _studentService.GetStudentsByDepartmentQuerable(request.Id);
            var paginatedStudents = await studentQueryable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            mappedDepartment.StudentList = paginatedStudents;

            return new ApiResponse<GetDepartmentByIdResponseModel>
            {
                Data = mappedDepartment,
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.GetDepartmentByID]
            };

        }
    }
}
