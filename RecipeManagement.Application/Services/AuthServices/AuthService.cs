using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeManagement.Application.Abstractions.IServices;
using RecipeManagement.Domain.Entities.DTOs;
using RecipeManagement.Domain.Entities.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RecipeManagement.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ResponseLogin> GenerateToken(RegisterLogin user)
        {
            if (user != null)
            {
                var model = new User()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Role = user.Role.ToLower(),
                };

                var permissions = new List<int>();

                if (model.Role == "user")
                {
                    permissions = new List<int>() { 2, 3 };
                }
                else if (model.Role == "chef")
                {
                    permissions = new List<int>() { 1, 2, 3, 4, 5 };
                }

                var jsonContent = JsonSerializer.Serialize(permissions);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, model.Role),
                    new Claim("Login", model.Login),
                    new Claim("UserID", model.Id.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent),
                };

                return await GenerateToken(claims);
            }

            return new ResponseLogin()
            {
                Token = "Unauthorized"
            };
        }

        public async Task<ResponseLogin> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(_config["JWT:ExpireDate"] ?? "1");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);


            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(exDate),
                signingCredentials: credentials);

            return new ResponseLogin()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
