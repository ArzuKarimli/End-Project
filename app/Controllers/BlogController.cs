using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.BlogPage;

namespace app.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var blogs= await _blogService.GetAllAsync();
            BlogVM model = new()
            {
                Blogs = blogs.ToList(),
            };
            return View(model);
        }
    }
}
