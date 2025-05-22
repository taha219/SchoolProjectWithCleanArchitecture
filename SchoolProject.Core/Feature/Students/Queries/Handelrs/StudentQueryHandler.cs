using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Wrappers;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Student.Queries.Handlers
{
    public class StudentQueryHandler :
                                       IRequestHandler<GetStudentListQuery, ApiResponse<List<GetStudentListResponse>>>,
                                       IRequestHandler<GetStudentByIdQuery, ApiResponse<GetSingleStudentResponse>>,
                                       IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>

    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetAllStudentsAsync();
            if (result == null)
            {
                return new ApiResponse<List<GetStudentListResponse>>
                {
                    IsSuccess = false,
                    Message = "No Students"
                };
            }
            var mappedStudents = _mapper.Map<List<GetStudentListResponse>>(result);

            return new ApiResponse<List<GetStudentListResponse>>
            {
                Data = mappedStudents,
                IsSuccess = true,
                Message = "All students retrieved successfully"
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
                    Message = "No student found with this ID"
                };
            }

            var mappedStudent = _mapper.Map<GetSingleStudentResponse>(student);

            return new ApiResponse<GetSingleStudentResponse>
            {
                Data = mappedStudent,
                IsSuccess = true,
                Message = "Student retrieved successfully"
            };
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SchoolProject.Data.Entities.Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudentId, e.Name, e.Address, e.Phone, e.Department.DName);
            var query = _studentService.GetStudentsListQuerable();
            var paginatedlist = await query.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedlist;
        }
    }
}