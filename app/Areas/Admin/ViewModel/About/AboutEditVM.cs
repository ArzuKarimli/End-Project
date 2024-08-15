namespace app.Areas.Admin.ViewModel.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
