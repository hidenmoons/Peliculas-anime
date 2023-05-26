using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendPeliculas.Entities;
namespace BackendPeliculas.Models
{
    public class MyDbContext: IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

        }
    }
}
