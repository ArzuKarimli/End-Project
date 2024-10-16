﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher:BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public bool IsMain { get; set; } = false;
        public ICollection<Course> Courses { get; set; }
    }
}
