using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManagement.API.Attributes;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Enums;
using RecipeManagement.Domain.Entities.Models;
using RecipeManagement.Domain.Entities.ViewModels;

namespace RecipeManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [IdentityFilter(Permission.CreateUser)]
        public async Task<ActionResult<User>> CreateUser(UserDTO model)
        {
            var result = await _userService.Create(model);

            return Ok(result);
        }

        [HttpGet]
        [IdentityFilter(Permission.GetAllUsers)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            var result = await _userService.GetAll();

            return Ok(result);
        }

        [HttpGet]
        [IdentityFilter(Permission.GetUserById)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var result = await _userService.GetById(id);

            return Ok(result);
        }

        [HttpPut]
        [IdentityFilter(Permission.UpdateUser)]
        public async Task<ActionResult<User>> UpdateUser(int id, UserDTO model)
        {
            var result = await _userService.Update(id, model);

            return Ok(result);
        }

        [HttpDelete]
        [IdentityFilter(Permission.DeleteUser)]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var result = await _userService.Delete(x => x.Id == id);

            if (!result)
            {
                return BadRequest("Something went wrong...");
            }

            return Ok("Deleted successfully");
        }
    }
}
