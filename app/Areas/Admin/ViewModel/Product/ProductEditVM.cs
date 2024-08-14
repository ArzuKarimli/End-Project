namespace app.Areas.Admin.ViewModel.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }
        public string Material { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public List<ProductImageVM> Images { get; set; }
        public List<IFormFile> NewImages { get; set; }
    }
}
