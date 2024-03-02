using RecipeManagement.Domain.Entities.DTOs;

namespace RecipeManagement.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<ResponseLogin> GenerateToken(RequestLogin user);
    }
}
