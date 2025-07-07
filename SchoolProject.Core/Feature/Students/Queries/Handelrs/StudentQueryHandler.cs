using System.Globalization;
using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Student.Queries.Handlers
{
    public class StudentQueryHandler : IRequestHandler<GetStudentListQuery, ApiResponse<List<GetStudentListResponse>>>,
                                       IRequestHandler<GetStudentByIdQuery, ApiResponse<GetSingleStudentResponse>>,
                                       IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>

    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper,
                                   IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<ApiResponse<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetAllStudentsAsync();
            if (result == null || result.Count == 0)
            {
                return new ApiResponse<List<GetStudentListResponse>>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.NotFoundStudent]
                };
            }
            var mappedStudents = _mapper.Map<List<GetStudentListResponse>>(result);

            return new ApiResponse<List<GetStudentListResponse>>
            {
                Data = mappedStudents,
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.GetStudentList]
            };
        }

        public async Task<ApiResponse<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            if (student == null)
            {
                return new ApiResponse<GetSingleStudentResponse>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.NotFoundStudent]
                };
            }
            var mappedStudent = _mapper.Map<GetSingleStudentResponse>(student);

            return new ApiResponse<GetSingleStudentResponse>
            {
                Data = mappedStudent,
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.GetStudent]
            };
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            Expression<Func<SchoolProject.Data.Entities.Student, GetStudentPaginatedListResponse>> expression =
                e => new GetStudentPaginatedListResponse(
                    e.StudID,
                    culture == "ar" ? e.NameAr : e.NameEn,
                    culture == "ar" ? e.AddressAr : e.AddressEn,
                    e.Phone,
                    culture == "ar" ? e.Department.DNameAr : e.Department.DNameEn
                );

            var filterquery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedlist = await filterquery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedlist;
        }
    }
}