using Microsoft.AspNetCore.Mvc;
using BackendPeliculas.Models;
using BackendPeliculas.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BackendPeliculas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        public TokenController(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> GenerateToken(AuthenticateRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);

            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Forbid();
            }

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Ok(new
            {
                AccessToken = jwt
            });
        }
       
        

    }
}
