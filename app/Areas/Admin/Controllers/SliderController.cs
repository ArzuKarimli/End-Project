using app.Areas.Admin.Helpers.Extentions;
using app.Areas.Admin.ViewModel.Slider;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;

        }
        [HttpGet]
     
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllWithInfoAsync();


            List<SliderVM> model = new List<SliderVM>();

            foreach (var item in sliders)
            {
                SliderVM result = new()
                {
                    Id = item.Id,
                    Image = item.Image,
                    SliderInfo = item.SliderInfo,
                };

                model.Add(result);
            }

            return View(model);
        }

        [HttpGet]
       
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = await _sliderService.GetByIdWithInfoAsync((int)id);
            if (slider is null) return NotFound();

            return View(new SliderDetailVM { Image = slider.Image,SliderInfo=slider.SliderInfo });

        }

        [HttpGet]
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            if (request.Images.Count == 0)
            {
                ModelState.AddModelError("Images", "Please upload at least one image.");
                return View(request);
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File must be only image format");
                    return View(request);
                }

                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Images", "Image size must be max 200 kb");
                    return View(request);
                }
            }

            var sliderList = new List<Slider>();

            foreach (var image in request.Images)
            {
                string path = image.GenerateFilePath(_env);
                await image.SaveFileToLocalAsync(path);

                var slider = new Slider
                {
                    Image = Path.GetFileName(path),
                    SliderInfo = new SliderInfo
                    {
                        Title = request.Title,
                        Description = request.Description
                    }
                };

                sliderList.Add(slider);
            }


            foreach (var slider in sliderList)
            {
                await _sliderService.AddAsync(slider);
            }

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _sliderService.GetByIdForSliderAsync((int)id);
            if (slider is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "images", slider.Image);
            path.DeleteFileFromLocal();
            await _sliderService.DeleteSliderAsync(slider);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetByIdWithInfoAsync((int)id);
            if (slider == null) return NotFound();        
            var model = new SliderEditVM
            {
                Id = slider.Id,
                Image = slider.Image,
                SliderInfo = slider.SliderInfo,
               
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderEditVM request)
        {
            

            var slider = await _sliderService.GetByIdWithInfoAsync(request.Id);
            if (slider == null) return NotFound();

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 kb");
                    return View(request);
                }

                string oldImagePath = Path.Combine(_env.WebRootPath, "images", slider.Image);
                oldImagePath.DeleteFileFromLocal();

                string newPath = request.NewImage.GenerateFilePath(_env);
                await request.NewImage.SaveFileToLocalAsync(newPath);
                slider.Image = Path.GetFileName(newPath);
            }

            slider.SliderInfo.Title = request.SliderInfo.Title;
            slider.SliderInfo.Description = request.SliderInfo.Description;

            await _sliderService.UpdateAsync(slider);

            return RedirectToAction(nameof(Index));
        }

    }
}

