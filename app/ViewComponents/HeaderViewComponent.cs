using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel;

namespace app.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService  _settingService;
        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _settingService.GetAllAsync();
            HeaderVM model = new()
            {
                Settings = settings
            };
            return View(model);
        }
    }
}
