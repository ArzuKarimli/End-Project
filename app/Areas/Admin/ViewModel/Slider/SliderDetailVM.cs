using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace app.Areas.Admin.ViewModel.Slider
{
    public class SliderDetailVM
    {
        [Required]
        public string Image { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
