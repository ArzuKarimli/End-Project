﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CourseCategory :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
