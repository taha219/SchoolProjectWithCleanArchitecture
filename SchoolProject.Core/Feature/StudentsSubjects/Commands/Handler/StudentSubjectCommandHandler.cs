using System.Net;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.StudentsSubjects.Commands.Models;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.StudentsSubjects.Commands.Handler
{
    public class StudentSubjectCommandHandler : IRequestHandler<AddStudentGradeCommand, ApiResponse<string>>,
                                               IRequestHandler<UpdateStudentGradeCommand, ApiResponse<string>>
    {
        private readonly IStudentSubjectService _studentSubjectService;
        private readonly INotificationService _notificationService;
        private readonly ISubjectReposatory _subjectReposatory;

        public StudentSubjectCommandHandler(IStudentSubjectService studentSubjectService,
                                                 INotificationService notificationService,
                                                    ISubjectReposatory subjectReposatory
                                                )
        {
            _studentSubjectService = studentSubjectService;
            _notificationService = notificationService;
            _subjectReposatory = subjectReposatory;
        }

        public async Task<ApiResponse<string>> Handle(AddStudentGradeCommand request, CancellationToken cancellationToken)
        {
            var isAdded = await _studentSubjectService.AddStudentGradeAsync(request.StudID, request.SubID, request.Grade);

            if (isAdded)
            {
                var subject = await _subjectReposatory.GetByIdAsync(request.SubID);
                if (subject != null)
                {
                    await _notificationService.NotifyStudentAsync(request.StudID, subject.localize(subject.SubjectNameAr, subject.SubjectNameEn), "إضافة");
                    return new ApiResponse<string>
                    {
                        IsSuccess = true,
                        Message = "Grade added successfully.",
                        StatusCode = HttpStatusCode.Created
                    };
                }
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "subject not found",
                };

            }

            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = "Failed to add grade.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<ApiResponse<string>> Handle(UpdateStudentGradeCommand request, CancellationToken cancellationToken)
        {
            var isUpdated = await _studentSubjectService.UpdateStudentGradeAsync(request.StudID, request.SubID, request.Grade);

            if (isUpdated)
            {
                var subject = await _subjectReposatory.GetByIdAsync(request.SubID);
                if (subject != null)
                {
                    await _notificationService.NotifyStudentAsync(request.StudID, subject.localize(subject.SubjectNameAr, subject.SubjectNameEn), "تعديل");
                    return new ApiResponse<string>
                    {
                        IsSuccess = true,
                        Message = "Grade edited successfully.",
                    };
                }
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "subject not found",
                };
            }

            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Grade added successfully.",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}