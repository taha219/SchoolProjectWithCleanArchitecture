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


namespace SchoolProject.Core.Feature.Student.Queries.Handelrs
{
    public class StudentHandler :ResponseHandler, IRequestHandler<GetStudentListQuery,Response<List<GetStudentListResponse>>>
    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper; 
        public StudentHandler(IStudentService studentService , IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var result= await _studentService.GetAllStudentsAsync();
            var mappedstds = _mapper.Map<List<GetStudentListResponse>>(result);
            return Success(mappedstds,"تم استرجاع كل الطلاب");
        }
    }
    
}
