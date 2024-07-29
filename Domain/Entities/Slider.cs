using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Slider : BaseEntity
    {
        public string Image { get; set; }
        public SliderInfo SliderInfo { get; set; }

    }
}
