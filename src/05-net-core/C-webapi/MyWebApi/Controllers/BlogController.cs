using Microsoft.AspNetCore.Mvc;
using MyWebApi.Ef;

namespace MyWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogDbContext context;

        public BlogController(BlogDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public object Get()
        {
            return context.Blogs.Select((c) => new
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            }).ToList();
        }

        [HttpGet("{title}")]
        public object GetByTitle(string title)
        {
            return context.Blogs.Where(b => b.Title == title).Select((c) => new
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            }).ToList();
        }

    }
}
