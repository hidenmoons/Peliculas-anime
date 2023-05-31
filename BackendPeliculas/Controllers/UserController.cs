using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        
        public UserController(UserManager<User> userManager) 
        {
            this.userManager= userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create_user(UserManager<User> userManager) 
        {
            var newUser = new User
            {
                Email = "test@demo.com",
                FirstName = "Test",
                LastName = "User",
                UserName = "test.demo"
            };
            var result = await userManager.CreateAsync(newUser,"12345678");
            return Ok(result);
        }
    }
}
