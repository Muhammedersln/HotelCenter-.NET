using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelCenter.Domain.Entities;

namespace HotelCenter.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Kraliyet Villası",
                    Description = "Kraliyet tarzında tasarlanmış lüks ve konforlu bir villa.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Premium Havuzlu Villa",
                    Description = "Özel havuzu ve modern olanaklarıyla premium bir villa deneyimi.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Lüks Havuzlu Villa",
                    Description = "Geniş yaşam alanı ve özel havuzuyla lüks bir tatil villası.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                }
                );
        }
    }
}
