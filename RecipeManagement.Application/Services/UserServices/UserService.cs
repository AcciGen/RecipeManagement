using RecipeManagement.Application.Abstractions;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Models;
using RecipeManagement.Domain.Entities.ViewModels;
using System.Linq.Expressions;

namespace RecipeManagement.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(UserDTO userDTO)
        {
            var user = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Login = userDTO.Login,
                Password = userDTO.Password,
                Role = userDTO.Role.ToLower(),
            };
            var result = await _userRepository.Create(user);

            return result;
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var users = await _userRepository.GetAll();

            var result = users.Select(model => new UserViewModel
            {
                Name = model.Name,
                Email = model.Email,
                Role = model.Role,
            });

            return result;
        }

        public async Task<User> GetById(int Id)
        {
            var result = await _userRepository.GetByAny(x => x.Id == Id);
            return result;
        }

        public async Task<User> GetByLogin(string login)
        {
            var result = await _userRepository.GetByAny(x => x.Login == login);
            return result;
        }

        public async Task<User> Update(int Id, UserDTO userDTO)
        {
            var res = await _userRepository.GetByAny(x => x.Id == Id);

            if (res != null)
            {
                var user = new User()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Login = userDTO.Login,
                    Password = userDTO.Password,
                    Role = userDTO.Role,
                };
                var result = await _userRepository.Update(user);

                return result;
            }

            return null!;
        }

        public async Task<bool> Delete(Expression<Func<User, bool>> expression)
        {
            var result = await _userRepository.Delete(expression);

            return result;
        }
    }
}
