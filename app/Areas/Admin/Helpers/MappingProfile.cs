using app.Areas.Admin.ViewModel.Course;

using AutoMapper;
using Domain.Entities;
using Service.ViewModel.TeacherPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Course, CourseVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              
                .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
               .ForMember(dest => dest.CourseCategory, opt => opt.MapFrom(src => src.CourseCategory != null ? src.CourseCategory.Name : string.Empty))
               .ForMember(dest => dest.TeacherNames, opt => opt.MapFrom(src => src.Teachers != null ? src.Teachers.Select(t => t.FullName).ToList() : new List<string>()));
            CreateMap<Course, TeacherPageVM>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
               .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
               .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
               .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
               .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
               .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
               .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
               .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Student))
               .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
               .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
              .ForMember(dest => dest.CourseCategory, opt => opt.MapFrom(src => src.CourseCategory != null ? src.CourseCategory.Name : string.Empty))
              .ForMember(dest => dest.TeacherNames, opt => opt.MapFrom(src => src.Teachers != null ? src.Teachers.Select(t => t.FullName).ToList() : new List<string>()));
            CreateMap<Course, CourseDetailVM>()
             
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
               .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

               .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
               .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
               .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
               .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
               .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
               .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Student))
               .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
               .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
              .ForMember(dest => dest.CourseCategory, opt => opt.MapFrom(src => src.CourseCategory != null ? src.CourseCategory.Name : string.Empty))
              .ForMember(dest => dest.TeacherNames, opt => opt.MapFrom(src => src.Teachers != null ? src.Teachers.Select(t => t.FullName).ToList() : new List<string>()));
      
            CreateMap<EditCourseVM, Course>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
              .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
              .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
              .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
              .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
              .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
              .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.StudentCount))
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
              .ForMember(dest => dest.CourseCategoryId, opt => opt.MapFrom(src => src.CourseCategoryId));
              
        }
    }
}