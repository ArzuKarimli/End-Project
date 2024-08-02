using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SlidersInfos { get; set; }
        public DbSet<Content> Services { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
               .HasMany(c => c.Teachers)
               .WithMany(t => t.Courses)
               .UsingEntity(j => j.ToTable("CourseTeacher"));

        }
    }
}
