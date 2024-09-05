using ToDoTask.Application.Interfaces;
using ToDoTask.Application.Services;
using ToDoTask.Infrastructure.Repositories;

namespace ToDoTask.API
{
    public static class ConfigureServices
     {
         public static void Configure(this IServiceCollection services)
         {
             services.AddScoped<IToDoService, ToDoService>();
             services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
             services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }  
}
