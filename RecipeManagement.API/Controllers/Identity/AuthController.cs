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
        public async Task<ActionResult<ResponseLogin>> Login(RequestLogin model)
        {
            var result = await _authService.GenerateToken(model);

            return Ok(result);
        }
    }
}
