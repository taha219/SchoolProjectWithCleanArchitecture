using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Concrete;

namespace SchoolProject.Core
{
    public static class  ModuleCoreDependencies 
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
