using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Models;
using RecipeManagement.Domain.Entities.ViewModels;
using System.Linq.Expressions;

namespace RecipeManagement.Application.Abstractions.IServices
{
    public interface IUserService
    {
        public Task<User> Create(UserDTO userDTO);
        public Task<IEnumerable<UserViewModel>> GetAll();
        public Task<User> GetById(int Id);
        public Task<User> GetByLogin(string email);
        public Task<User> Update(int Id, UserDTO userDTO);
        public Task<bool> Delete(Expression<Func<User, bool>> expression);
    }
}
