using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.TeacherPage
{
    public class EditCourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public int Seat { get; set; }
        public string Level { get; set; }
        public int Lesson { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public int Rating { get; set; }
        public int Class { get; set; }
        public int StudentCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseCategoryId { get; set; }
      
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
    }
}
