using RecipeManagement.Domain.Entities.DTOs;

namespace RecipeManagement.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<string> SignUpAsync(RegisterLogin user);
        public Task<ResponseLogin> LogInAsync(RequestLogin user);
    }
}
