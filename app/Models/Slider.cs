using Domain.Entities;

namespace app.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
