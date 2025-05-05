using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Concrete;

namespace SchoolProject.Services
{
    public static class ModuleServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            return services;
        }
    }
}
