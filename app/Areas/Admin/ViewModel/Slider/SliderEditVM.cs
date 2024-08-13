using Domain.Entities;

namespace app.Areas.Admin.ViewModel.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
