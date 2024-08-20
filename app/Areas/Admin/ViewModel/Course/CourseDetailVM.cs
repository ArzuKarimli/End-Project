namespace app.Areas.Admin.ViewModel.Course
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public int Seat { get; set; }
        public string Level { get; set; }
        public int Lesson { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public int Class { get; set; }
        public int StudentCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CourseCategory { get; set; }
        public List<string> TeacherNames { get; set; }
    }
}
