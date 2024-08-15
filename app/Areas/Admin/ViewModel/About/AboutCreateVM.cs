namespace app.Areas.Admin.ViewModel.About
{
    public class AboutCreateVM
    {
        public string SectionName { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
