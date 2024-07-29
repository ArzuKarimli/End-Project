﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SliderInfo :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
