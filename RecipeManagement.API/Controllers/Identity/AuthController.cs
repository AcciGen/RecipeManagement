using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;

namespace RecipeManagement.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseLogin>> Login(RegisterLogin model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                var result = await _authService.GenerateToken(model);

                return Ok(result);
            }

            return BadRequest("Passwords are not the same...");
        }
    }
}
