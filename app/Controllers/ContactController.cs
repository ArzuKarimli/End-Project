using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Contact;

namespace app.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ISettingService _settingService;
        public ContactController(IContactService contactService, ISettingService settingService)
        {
            _contactService = contactService;
            _settingService = settingService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> settings = await _settingService.GetAllAsync();
            ContactVM model = new()
            {
                Settings = settings
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactVM review)
        {
            if (!ModelState.IsValid)
            {
                await _contactService.CreateMessage(new Contact
                {
                    Name = review.Name,
                    Email = review.Email,
                    Message = review.Message
                });              
              return RedirectToAction(nameof(Index));
            }
          
          
            return RedirectToAction(nameof(Index));
        }
    }
}

