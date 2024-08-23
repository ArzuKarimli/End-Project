using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ISliderInfoRepository,SliderInfoRepository>();
            services.AddScoped<ISliderRepository,SliderRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IAboutRepository,AboutRepository>();
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            return services;
        }
    }
}
