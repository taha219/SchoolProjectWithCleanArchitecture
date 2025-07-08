using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Implementations;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Concrete;

namespace SchoolProject.Services
{
    public static class ModuleServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IAuthenticationUserService, AuthenticationUserService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailsService, EmailsService>();
            return services;
        }
    }
}
