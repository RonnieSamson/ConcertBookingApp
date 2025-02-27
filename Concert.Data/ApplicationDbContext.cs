﻿using Concert.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Concert.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<ConcertEntity> Concerts { get; set; }
        public virtual DbSet<Performance> Performances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🟢 User-tabellen
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>().Property(u => u.Id)
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

            // 🟢 Booking-tabellen
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Booking>().Property(b => b.Id)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>().Property(b => b.UserId)
                .HasColumnType("nvarchar(36)")
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .IsRequired();

            // Performances tabellen
            modelBuilder.Entity<Performance>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Performance>().Property(p => p.Id)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .IsRequired();
            modelBuilder.Entity<Performance>().Property(p => p.StartTime)
                .IsRequired();
            modelBuilder.Entity<Performance>()
                .Property(p => p.EndTime)
                .IsRequired();
            modelBuilder.Entity<Performance>()
                .Property(p => p.ConcertId)
                .IsRequired();
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Concert)
                .WithMany(c => c.Performances)
                .HasForeignKey(p => p.ConcertId)
                .IsRequired();


            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // 🟢 Seeda konserter först!
            var concert1 = new ConcertEntity
            {
                ConcertId = "1",
                Title = "Rock Night",
                Description = "A night of rock music"
            };

            var concert2 = new ConcertEntity
            {
                ConcertId = "2",
                Title = "Jazz Night",
                Description = "A night of jazz music"
            };

            var concert3 = new ConcertEntity
            {
                ConcertId = "3",
                Title = "Pop Night",
                Description = "A night of pop music"
            };



            builder.Entity<ConcertEntity>().HasData(concert1, concert2, concert3);

            // 🟢 Seeda användare
            var user1 = new User
            {
                Id = "1",
                Name = "John",
                Email = "John123@example.com",
                Password = "John123",
            };

            var user2 = new User
            {
                Id = "2",
                Name = "Bob Bengtsson",
                Email = "bob@example.com",
                Password = "anotherpassword"
            };

            var user3 = new User
            {
                Id = "3",
                Name = "Alice",
                Email = "alice@example.com",
                Password = "securepassword"
            };

            builder.Entity<User>().HasData(user1, user2, user3);

            // 🟢 Seeda bokningar (❌ Ta bort ID!)
            var booking1 = new Booking
            {
                Id = "1",
                BookingDate = new DateTime(2024, 1, 1, 12, 0, 0),
                UserId = "1",
                ConcertId = "1"  // 🔹 Nu finns denna concert i `Concerts`
            };

            var booking2 = new Booking
            {
                Id = "2",
                BookingDate = new DateTime(2024, 1, 1, 12, 0, 0),
                UserId = "2",
                ConcertId = "2"
            };

            var booking3 = new Booking
            {
                Id = "3",
                BookingDate = new DateTime(2024, 1, 1, 12, 0, 0),
                UserId = "3",
                ConcertId = "3"
            };

            builder.Entity<Booking>().HasData(booking1, booking2, booking3);

            Performance performance1 = new Performance
            {
                Id = "1",
                StartTime = new DateTime(2024, 1, 1, 12, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 12, 0, 0),
                ConcertId = "1"
            };

            Performance performance2 = new Performance
            {
                Id = "2",
                StartTime = new DateTime(2024, 1, 1, 12, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 12, 0, 0),
                ConcertId = "2"
            };

            Performance performance3 = new Performance
            {
                Id = "3",
                StartTime = new DateTime(2024, 1, 1, 12, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 12, 0, 0),
                ConcertId = "3"
            };

            builder.Entity<Performance>().HasData(performance1, performance2, performance3);
        }

    }
}
