using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Student.Queries.Handlers
{
    public class StudentQueryHandler : IRequestHandler<GetStudentListQuery, ApiResponse<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIdQuery, ApiResponse<GetSingleStudentResponse>>
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
    }
}
