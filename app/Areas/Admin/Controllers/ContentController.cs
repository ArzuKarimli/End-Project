using app.Areas.Admin.ViewModel.Content;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ContentController : Controller
    {

        private readonly IContentService _contentService;
        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            var datas = await _contentService.GetAllAsync();
            return View(datas.Select(m => new ContentVM { Id = m.Id, Description = m.Description, Icon = m.Icon, Title = m.Title }).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Content data = await _contentService.GetByIdAsync((int)id);
            if (data == null) return NotFound();
            await _contentService.DeleteAsync(data);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Content data = await _contentService.GetByIdAsync((int)id);
            if (data == null) return NotFound();

            return View(new ContentDetailVM { Id = data.Id, Description = data.Description, Title = data.Title, Icon = data.Icon });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create(ContentCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _contentService.CreateAsync(new Content { Description = request.Description, Title = request.Title, Icon = request.Icon });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return BadRequest();
            Content data = await _contentService.GetByIdAsync((int)id);
            if (data == null) return NotFound();
            return View(new ContentEditVM { Title=data.Title,Description=data.Description,Icon=data.Icon});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, ContentEditVM request)
        {
            if (id == null) return BadRequest();
            Content data = await _contentService.GetByIdAsync((int)id);
            if (data == null) return NotFound();

      
            data.Title = request.Title;
            data.Description = request.Description;
            data.Icon = request.Icon;

            await _contentService.UpdateAsync(data);

            return RedirectToAction(nameof(Index));
        }
    }
}
