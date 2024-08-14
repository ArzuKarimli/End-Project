namespace app.Areas.Admin.ViewModel.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal Weight { get; set; }
        public string Material { get; set; }
        public int CategoryId { get; set; }
        
        public decimal Price { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
