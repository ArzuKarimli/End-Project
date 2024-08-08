using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.TeacherPage
{
    public class TeacherVM
    {
        public List<Teacher> Teachers { get; set; }
        public List<CourseCategory> CourseCategories { get; set; }
    }
}
