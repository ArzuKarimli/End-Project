using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace app.Areas.Admin.ViewModel.Slider
{
    public class SliderVM
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
