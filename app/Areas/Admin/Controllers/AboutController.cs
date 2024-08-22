using app.Areas.Admin.Helpers.Extentions;
using app.Areas.Admin.ViewModel.About;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(IAboutService aboutService, IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var datas= await _aboutService.GetAllAsync();
            return View(datas.Select(m => new AboutVM{ Id=m.Id,SectionName=m.SectionName,Description=m.Description,Image=m.Image}).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            About data = await _aboutService.GetByIdAsync((int)id);
            if (data == null) return NotFound();
            return View(new AboutDetailVM { Description = data.Description, SectionName = data.SectionName, Image = data.Image });
                
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
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var imageFile = request.Images.FirstOrDefault();

            if (imageFile != null)
            {
                if (!imageFile.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 KB");
                    return View(request);
                }
                if (!imageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be an image");
                    return View(request);
                }
            }

            string fileName = null;
            if (imageFile != null)
            {
                fileName = Guid.NewGuid().ToString() + "-" + imageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                await imageFile.SaveFileToLocalAsync(path);
            }

            About about = new()
            {
                SectionName = request.SectionName,
                Description = request.Description,
                Image = fileName  
            };

            await _aboutService.CreateAsync(about);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            About data = await _aboutService.GetByIdAsync((int)id);
            if (data == null) return NotFound();
           
             string path = Path.Combine(_env.WebRootPath, "images", data.Image);
             path.DeleteFileFromLocal();
            
            await _aboutService.DeleteAsync(data);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            About data = await _aboutService.GetByIdAsync((int)id);
            if (data == null) return NotFound();

            AboutEditVM model = new()
            {
                SectionName = data.SectionName,
                Description = data.Description,
                Image = data.Image,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id == null) return BadRequest();

            About data = await _aboutService.GetByIdAsync((int)id);
            if (data == null) return NotFound();

            if (request.Images != null && request.Images.Any())
            {
                string oldImagePath = Path.Combine(_env.WebRootPath, "assets/images", data.Image);
                oldImagePath.DeleteFileFromLocal();

                var imageFile = request.Images.FirstOrDefault();
                if (imageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + imageFile.FileName;
                    string newPath = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                    await imageFile.SaveFileToLocalAsync(newPath);
                    data.Image = fileName;
                }
            }
            data.SectionName = request.SectionName;
            data.Description = request.Description;

            await _aboutService.UpdateAsync(data);

            return RedirectToAction(nameof(Index));
        }

    }
}
