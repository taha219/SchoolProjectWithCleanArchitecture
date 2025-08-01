﻿using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Concrete;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Reposatories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentReposatory, StudentReposatory>();
            services.AddTransient<IDepartmentReposatory, DepartmentReposatory>();
            services.AddTransient<IInstructorReposatory, InstructorReposatory>();
            services.AddTransient<ISubjectReposatory, SubjectReposatory>();
            services.AddTransient<IStudentSubjectReposatory, StudentSubjectReposatory>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}