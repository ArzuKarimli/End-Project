using Domain.Entities;

namespace app.Areas.Admin.ViewModel.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public decimal Weight { get; set; }
        public string Material { get; set; }
        public string Category { get; set; }
        public int SalesCount { get; set; } = 0;
        public decimal Price { get; set; }
    }
}
