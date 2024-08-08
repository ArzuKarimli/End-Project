using Microsoft.AspNetCore.Http;
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
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IAboutService,AboutService>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService,CartService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

    }
}