
using BLL.Base;
using BLL.Interface;
using DAL.Base;
using DAL.Interface;
using Microsoft.Extensions.DependencyInjection;
using Site.Service;

namespace Site.Middleware
{
    public static class Ioc
    {
        public static IServiceCollection AddDependecy(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}
