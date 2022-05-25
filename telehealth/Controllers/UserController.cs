using telehealth.Context;
using telehealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace telehealth.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext context;

        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await context.Users.ToListAsync();

            return Ok(users);
        }

    }
}
