using System.ComponentModel.DataAnnotations;

namespace app.Areas.Admin.ViewModel.ProductCategoryVM
{
    public class CategoryDetailVM
    {
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
