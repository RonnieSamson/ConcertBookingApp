using ConcertBookingApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Concert> Concerts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed-data för Concert-tabellen
            modelBuilder.Entity<Concert>().HasData(
                new Concert
                {
                    Id = 1,
                    Title = "First Concert",
                    Description = "This is the first concert"
                },
                new Concert
                {
                    Id = 2,
                    Title = "Second Concert",
                    Description = "This is the second concert"
                }
            );
        }
    }
}
