using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace app.Areas.Admin.ViewModel.Slider
{
    public class SliderCreateVM
    {
        public int Id { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }

        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(40, ErrorMessage = "Length must be max 20")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(40, ErrorMessage = "Length must be max 20")]
        public string Description { get; set; }
    }
}
