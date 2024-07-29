using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddserviceLayer(this IServiceCollection services)
        {

            services.AddScoped<ISliderInfoService,SliderInfoService>();
            services.AddScoped<ISliderService,SliderService>();
            return services;
        }

    }
}