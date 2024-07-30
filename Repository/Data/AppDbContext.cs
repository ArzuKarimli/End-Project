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
        DbSet<Slider> Sliders { get; set; }
        DbSet<SliderInfo> SlidersInfos { get; set; }
        DbSet<Content> Services { get; set; }
        DbSet<About> Abouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Content>()
             .ToTable("Content");
        }
    }
}
