using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.CoursePage
{
    public class CourseDetailVM
    {
        public Course Course { get; set; }
        public List<Course> Courses { get; set; }
        
    }
}
