using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel;

namespace app.ViewComponents
{
    
    public class FooterViewComponent : ViewComponent       
    {
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings= await _settingService.GetAllAsync();
            FooterVM model = new()
            {
                Settings = settings
            };
            return View(model);
        }
    }
}
