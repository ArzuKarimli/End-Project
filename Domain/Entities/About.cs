﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string History { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string Image { get; set; }
        public string VideoUrl { get; set; }
    }
}
