using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel;

namespace app.ViewComponents
{
    public class BaseHeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public BaseHeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _settingService.GetAllAsync();
            BaseHeaderVM model = new()
            {
                Settings = settings
            };
            return View(model);
        }
    }
}
