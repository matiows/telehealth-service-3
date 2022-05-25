using telehealth.Context;
using telehealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using telehealth.DTOs;
using telehealth.Services;
using System.Net;

namespace telehealth.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly DataContext context;

        private readonly IUserService _userService;

        public BlogController(DataContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;

        }

        [HttpGet("{id}")]
        public ActionResult<Blog> GetOne(int id)
        {
            var blog = context.Blogs
                .Where(blog => blog.BlogId == id)
                .Include(blog => blog.Comments)
                .FirstOrDefault();

            if (blog == null) return NotFound("Blog Not Found.");

            return Ok(blog);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Blog>>> GetByUser(int userId)
        {
            var blogs = await context.Blogs
                .Where(blog => blog.AuthorId == userId)
                .Include(blog => blog.Comments)
                .ToListAsync();

            if (blogs == null) return NotFound("No Blog Found.");

            return Ok(blogs);
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetAll()
        {
            var blogs = await context.Blogs.ToListAsync();

            return Ok(blogs);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Post(CreateBlogDTO blogDTO)
        {
            Blog blog = new();
            await _userService.CheckUser(blogDTO.AuthorId);
            blog.AuthorId = blogDTO.AuthorId;
            blog.Title = blogDTO.Title;
            blog.Body = blogDTO.Body;

            context.Blogs.Add(blog);

            await context.SaveChangesAsync();

            return Ok(blog);
        }
    }
}
