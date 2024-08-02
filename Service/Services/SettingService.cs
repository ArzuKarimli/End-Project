using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            var settings = await _settingRepository.GetAllAsync();
            return settings.ToDictionary(setting => setting.Key, setting => setting.Value);
        }
    }
}
