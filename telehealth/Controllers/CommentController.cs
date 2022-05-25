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
    public class CommentController : Controller
    {
        private readonly DataContext context;

        private readonly IUserService _userService;

        public CommentController(DataContext context, IUserService userService)
        {

            this.context = context;
            _userService = userService;

        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetOne(int id)
        {
            var comment = context.Comments
                .Where(comment => comment.CommentId == id)
                .FirstOrDefault();

            if (comment == null) return NotFound("Comment Not Found.");

            return Ok(comment);
        }

        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<List<Comment>>> GetByBlog(int blogId)
        {
            var comments = await context.Comments
                .Where(comment => comment.BlogId == blogId)
                .ToListAsync();

            return Ok(comments);
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAll()
        {
            var comments = await context.Comments.ToListAsync();

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Post(CreateCommentDTO commentDTO)
        {
            var blog = context.Blogs
                .Where(blog => blog.BlogId == commentDTO.BlogId)
                .Include(blog => blog.Comments)
                .FirstOrDefault();

            if (blog == null) return NotFound("Blog Not Found.");

            Comment comment = new();
            comment.BlogId = blog.BlogId;
            await _userService.CheckUser(commentDTO.CommentorId);
            comment.CommentorId = commentDTO.CommentorId;
            comment.Body = commentDTO.Body;

            context.Comments.Add(comment);

            await context.SaveChangesAsync();
            
            blog.Comments.Add(comment);

            await context.SaveChangesAsync();

            return Ok(comment);
        }
    }
}
