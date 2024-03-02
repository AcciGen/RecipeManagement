using Microsoft.Extensions.DependencyInjection;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Application.Services.AuthServices;
using RecipeManagement.Application.Services.RecipeServices;
using RecipeManagement.Application.Services.UserServices;

namespace RecipeManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
