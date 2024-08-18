using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel
{
    public class HomeVM
    {
       public List<Content> Contents { get; set; }
       public List<About> Abouts { get; set; }
        public List<Course> Courses { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Product> Products { get; set; }
    }
}
