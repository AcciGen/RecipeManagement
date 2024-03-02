using RecipeManagement.Application.Abstractions;
using RecipeManagement.Domain.Entities.Models;
using RecipeManagement.Infrastructure.Persistance;

namespace RecipeManagement.Infrastructure.BaseRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RecipeManagementDbContext context) : base(context)
        {
        }
    }
}
