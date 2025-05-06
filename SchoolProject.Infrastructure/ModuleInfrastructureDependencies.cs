using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Concrete;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentReposatory, StudentReposatory>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}