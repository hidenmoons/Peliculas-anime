using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BackendPeliculas.Controllers
{
  
        [ApiController]
        [Route("[controller]")]
        [Authorize]
        public class UserInfoController : ControllerBase
        {
            private readonly IHttpContextAccessor contextAccessor;

            public UserInfoController(IHttpContextAccessor contextAccessor)
            {
                this.contextAccessor = contextAccessor;
            }

            [HttpGet]
            public IActionResult GetUserInfo()
            {
                var user = contextAccessor.HttpContext.User;

                return Ok(new
                {
                    Claims = user.Claims.Select(s => new
                    {
                        s.Type,
                        s.Value
                    }).ToList(),
                    user.Identity.Name,
                    IsAuthenticated = user.Identity.IsAuthenticated,
                    AuthenticationType = user.Identity.AuthenticationType
                });
            }
        }
    
}
