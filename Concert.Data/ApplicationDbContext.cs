using Concert.Data.Entity;
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

            // User-tabellen
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

            // Booking-tabellen
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Booking>().Property(b => b.Id)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>().Property(b => b.PerformanceId)
                .HasColumnType("nvarchar(36)")
                .IsRequired();

            modelBuilder.Entity<Booking>().Property(b => b.CustomerName)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Booking>().Property(b => b.CustomerEmail)
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Performance)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PerformanceId)
                .IsRequired();

            // ConcertEntity-tabellen
            modelBuilder.Entity<ConcertEntity>()
                .HasKey(c => c.ConcertId);

            modelBuilder.Entity<ConcertEntity>().Property(c => c.ConcertId)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<ConcertEntity>().Property(c => c.Title)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<ConcertEntity>().Property(c => c.Description)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500);

            // Performance-tabellen
            modelBuilder.Entity<Performance>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Performance>().Property(p => p.Id)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<Performance>().Property(p => p.StartTime)
                .IsRequired();

            modelBuilder.Entity<Performance>().Property(p => p.EndTime)
                .IsRequired();

            modelBuilder.Entity<Performance>().Property(p => p.Location)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Performance>().Property(p => p.ConcertId)
                .HasColumnType("nvarchar(36)")
                .HasMaxLength(36)
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
            // Seeda konserter
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

            // Seeda användare
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

            // Seeda performances
            var performance1 = new Performance
            {
                Id = "1",
                StartTime = new DateTime(2024, 1, 1, 12, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 14, 0, 0),
                Location = "Göteborg Arena", // ✅ KRÄVS: Location för varje Performance
                ConcertId = "1"
            };
            var performance2 = new Performance
            {
                Id = "2",
                StartTime = new DateTime(2024, 1, 2, 18, 0, 0),
                EndTime = new DateTime(2024, 1, 2, 20, 0, 0),
                Location = "Stockholm Globen", // ✅ KRÄVS: Location för varje Performance
                ConcertId = "2"
            };
            var performance3 = new Performance
            {
                Id = "3",
                StartTime = new DateTime(2024, 1, 3, 19, 0, 0),
                EndTime = new DateTime(2024, 1, 3, 21, 0, 0),
                Location = "Malmö Live", // ✅ KRÄVS: Location för varje Performance
                ConcertId = "3"
            };
            builder.Entity<Performance>().HasData(performance1, performance2, performance3);

            
            var booking1 = new Booking
            {
                Id = "1",
                BookingDate = new DateTime(2024, 1, 1, 12, 0, 0),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                PerformanceId = "1"
                
            };
            var booking2 = new Booking
            {
                Id = "2",
                BookingDate = new DateTime(2024, 1, 2, 18, 0, 0),
                CustomerName = "Jane Smith",
                CustomerEmail = "jane.smith@example.com",
                PerformanceId = "2"
            };
            var booking3 = new Booking
            {
                Id = "3",
                BookingDate = new DateTime(2024, 1, 3, 19, 0, 0),
                CustomerName = "Bob Johnson",
                CustomerEmail = "bob.johnson@example.com",
                PerformanceId = "3"
            };
            builder.Entity<Booking>().HasData(booking1, booking2, booking3);
        }
    }
}