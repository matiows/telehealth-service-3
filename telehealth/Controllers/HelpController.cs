using telehealth.Context;
using telehealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using telehealth.DTOs;
using telehealth.Services;

namespace telehealth.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class HelpController : Controller
    {

        private readonly DataContext context;

        private readonly IUserService _userService;

        public HelpController(DataContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;

        }

        [HttpGet("{id}")]
        public ActionResult<Help> GetOne(int id)
        {
            var help = context.Helps
                .Where(help => help.HelpId == id)
                .Include(help => help.Comments)
                .FirstOrDefault();

            if (help == null) return NotFound("Help Not Found.");

            return Ok(help);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Help>>> GetByUser(int userId)
        {
            var helps = await context.Helps
                .Where(help => help.RequestorId == userId)
                .Include(help => help.Comments)
                .ToListAsync();

            if (helps == null) return NotFound("No Help Found.");

            return Ok(helps);
        }

        [HttpGet("random")]
        public async Task<ActionResult<Help>> GetRandom()
        {
            var random = new Random();

            var helps = await context.Helps
                .Where(help => help.Status == HELPSTATUS.APPROVED)
                .Include(help => help.Comments)
                .ToListAsync();

            int index = random.Next(helps.Count);

            if (helps == null) return NotFound("No Help Found.");

            return Ok(helps[index]);
        }

        [HttpGet("requested")]
        public async Task<ActionResult<Help>> GetRequested()
        {
            var helps = await context.Helps
                .Where(help => help.Status == HELPSTATUS.REQUESTED)
                .Include(help => help.Comments)
                .ToListAsync();

            if (helps == null) return NotFound("No Help Found.");

            return Ok(helps);
        }

        [HttpGet]
        public async Task<ActionResult<List<Help>>> GetAll()
        {
            var helps = await context.Helps.ToListAsync();

            return Ok(helps);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<Help>> Approve(int helpId)
        {
            var help = await context.Helps.FindAsync(helpId);

            if (help == null) return NotFound("Help Not Found.");

            help.Status = HELPSTATUS.APPROVED;

            await context.SaveChangesAsync();

            return Ok(help);
        }

        [HttpPost("decline")]
        public async Task<ActionResult<Help>> Decline(int helpId)
        {
            var help = await context.Helps.FindAsync(helpId);

            if (help == null) return NotFound("Help Not Found.");

            help.Status = HELPSTATUS.DECLINED;

            await context.SaveChangesAsync();

            return Ok(help);
        }

        [HttpPost("helped")]
        public async Task<ActionResult<Help>> Helped(int helpId)
        {
            var help = await context.Helps.FindAsync(helpId);

            if (help == null) return NotFound("Help Not Found.");

            help.Status = HELPSTATUS.HELPED;

            await context.SaveChangesAsync();

            return Ok(help);
        }

        [HttpPost]
        public async Task<ActionResult<Help>> Post(CreateHelpDTO helpDTO)
        {
            Help help = new();
            await _userService.CheckUser(helpDTO.RequestorId);
            help.RequestorId = helpDTO.RequestorId;
            help.Body = helpDTO.Body;

            context.Helps.Add(help);

            await context.SaveChangesAsync();

            return Ok(help);
        }

    }
}
