using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Concert.Data.Entity;


namespace Concert.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<User>().Property(u => u.ID)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId) 
                .IsRequired(); 

        }
    }
}
