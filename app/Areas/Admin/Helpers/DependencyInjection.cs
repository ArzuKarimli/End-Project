using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
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
        public static IServiceCollection AddMappingService(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

    }
}