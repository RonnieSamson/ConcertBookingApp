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
            // Seeda konserter - fler och mer varierade
            var concert1 = new ConcertEntity
            {
                ConcertId = "1",
                Title = "Rock Night",
                Description = "A night of rock music with legendary bands"
            };
            var concert2 = new ConcertEntity
            {
                ConcertId = "2",
                Title = "Jazz Night",
                Description = "Smooth jazz evening with world-class musicians"
            };
            var concert3 = new ConcertEntity
            {
                ConcertId = "3",
                Title = "Pop Night",
                Description = "The biggest pop hits performed live"
            };
            var concert4 = new ConcertEntity
            {
                ConcertId = "4",
                Title = "Classical Symphony",
                Description = "Beautiful classical music performed by symphony orchestra"
            };
            var concert5 = new ConcertEntity
            {
                ConcertId = "5",
                Title = "Electronic Dance Festival",
                Description = "High-energy electronic music and DJ performances"
            };
            var concert6 = new ConcertEntity
            {
                ConcertId = "6",
                Title = "Acoustic Unplugged",
                Description = "Intimate acoustic performances by indie artists"
            };
            var concert7 = new ConcertEntity
            {
                ConcertId = "7",
                Title = "Metal Mayhem",
                Description = "Heavy metal concert featuring brutal bands"
            };
            var concert8 = new ConcertEntity
            {
                ConcertId = "8",
                Title = "Country Music Night",
                Description = "Country classics and modern hits"
            };
            builder.Entity<ConcertEntity>().HasData(concert1, concert2, concert3, concert4, concert5, concert6, concert7, concert8);

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

            // Seeda performances - många fler med olika datum och städer
            var currentYear = DateTime.Now.Year;
            var performances = new List<Performance>
            {
                // Rock Night performances
                new Performance { Id = "1", StartTime = new DateTime(currentYear, 3, 15, 19, 0, 0), EndTime = new DateTime(currentYear, 3, 15, 22, 0, 0), Location = "Göteborg Arena", ConcertId = "1" },
                new Performance { Id = "2", StartTime = new DateTime(currentYear, 3, 20, 20, 0, 0), EndTime = new DateTime(currentYear, 3, 20, 23, 0, 0), Location = "Stockholm Globen", ConcertId = "1" },
                new Performance { Id = "3", StartTime = new DateTime(currentYear, 3, 25, 18, 30, 0), EndTime = new DateTime(currentYear, 3, 25, 21, 30, 0), Location = "Malmö Live", ConcertId = "1" },
                
                // Jazz Night performances
                new Performance { Id = "4", StartTime = new DateTime(currentYear, 4, 5, 19, 30, 0), EndTime = new DateTime(currentYear, 4, 5, 22, 0, 0), Location = "Konserthuset Stockholm", ConcertId = "2" },
                new Performance { Id = "5", StartTime = new DateTime(currentYear, 4, 10, 20, 0, 0), EndTime = new DateTime(currentYear, 4, 10, 22, 30, 0), Location = "Göteborgs Konserthus", ConcertId = "2" },
                new Performance { Id = "6", StartTime = new DateTime(currentYear, 4, 15, 18, 0, 0), EndTime = new DateTime(currentYear, 4, 15, 21, 0, 0), Location = "Malmö Opera", ConcertId = "2" },
                
                // Pop Night performances
                new Performance { Id = "7", StartTime = new DateTime(currentYear, 5, 1, 19, 0, 0), EndTime = new DateTime(currentYear, 5, 1, 22, 30, 0), Location = "Friends Arena", ConcertId = "3" },
                new Performance { Id = "8", StartTime = new DateTime(currentYear, 5, 8, 18, 0, 0), EndTime = new DateTime(currentYear, 5, 8, 21, 0, 0), Location = "Scandinavium", ConcertId = "3" },
                new Performance { Id = "9", StartTime = new DateTime(currentYear, 5, 15, 19, 30, 0), EndTime = new DateTime(currentYear, 5, 15, 22, 0, 0), Location = "Malmö Arena", ConcertId = "3" },
                
                // Classical Symphony performances
                new Performance { Id = "10", StartTime = new DateTime(currentYear, 6, 3, 19, 0, 0), EndTime = new DateTime(currentYear, 6, 3, 21, 30, 0), Location = "Konserthuset Stockholm", ConcertId = "4" },
                new Performance { Id = "11", StartTime = new DateTime(currentYear, 6, 10, 18, 30, 0), EndTime = new DateTime(currentYear, 6, 10, 21, 0, 0), Location = "Göteborgs Konserthus", ConcertId = "4" },
                
                // Electronic Dance Festival performances
                new Performance { Id = "12", StartTime = new DateTime(currentYear, 7, 12, 20, 0, 0), EndTime = new DateTime(currentYear, 7, 13, 2, 0, 0), Location = "Ullevi Göteborg", ConcertId = "5" },
                new Performance { Id = "13", StartTime = new DateTime(currentYear, 7, 19, 19, 0, 0), EndTime = new DateTime(currentYear, 7, 20, 1, 0, 0), Location = "Stockholm Stadion", ConcertId = "5" },
                new Performance { Id = "14", StartTime = new DateTime(currentYear, 7, 26, 18, 0, 0), EndTime = new DateTime(currentYear, 7, 27, 0, 0, 0), Location = "Malmö Festivalen", ConcertId = "5" },
                
                // Acoustic Unplugged performances
                new Performance { Id = "15", StartTime = new DateTime(currentYear, 8, 5, 19, 0, 0), EndTime = new DateTime(currentYear, 8, 5, 21, 0, 0), Location = "Cirkus Stockholm", ConcertId = "6" },
                new Performance { Id = "16", StartTime = new DateTime(currentYear, 8, 12, 18, 30, 0), EndTime = new DateTime(currentYear, 8, 12, 20, 30, 0), Location = "Liseberg Stora Scen", ConcertId = "6" },
                new Performance { Id = "17", StartTime = new DateTime(currentYear, 8, 19, 19, 30, 0), EndTime = new DateTime(currentYear, 8, 19, 21, 30, 0), Location = "Malmö Live", ConcertId = "6" },
                
                // Metal Mayhem performances
                new Performance { Id = "18", StartTime = new DateTime(currentYear, 9, 7, 19, 0, 0), EndTime = new DateTime(currentYear, 9, 7, 23, 0, 0), Location = "Hovet Stockholm", ConcertId = "7" },
                new Performance { Id = "19", StartTime = new DateTime(currentYear, 9, 14, 18, 0, 0), EndTime = new DateTime(currentYear, 9, 14, 22, 0, 0), Location = "Partille Arena", ConcertId = "7" },
                
                // Country Music Night performances
                new Performance { Id = "20", StartTime = new DateTime(currentYear, 10, 4, 19, 0, 0), EndTime = new DateTime(currentYear, 10, 4, 22, 0, 0), Location = "Gröna Lund Stockholm", ConcertId = "8" },
                new Performance { Id = "21", StartTime = new DateTime(currentYear, 10, 11, 18, 30, 0), EndTime = new DateTime(currentYear, 10, 11, 21, 30, 0), Location = "Liseberg Stora Scen", ConcertId = "8" },
                new Performance { Id = "22", StartTime = new DateTime(currentYear, 10, 18, 19, 30, 0), EndTime = new DateTime(currentYear, 10, 18, 22, 30, 0), Location = "Malmö Live", ConcertId = "8" }
            };
            
            builder.Entity<Performance>().HasData(performances.ToArray());

            // Seeda några exempel-bokningar
            var booking1 = new Booking
            {
                Id = "1",
                BookingDate = new DateTime(currentYear, 2, 15, 12, 0, 0),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                PerformanceId = "1"
            };
            var booking2 = new Booking
            {
                Id = "2",
                BookingDate = new DateTime(currentYear, 2, 20, 18, 0, 0),
                CustomerName = "Jane Smith",
                CustomerEmail = "jane.smith@example.com",
                PerformanceId = "4"
            };
            var booking3 = new Booking
            {
                Id = "3",
                BookingDate = new DateTime(currentYear, 3, 1, 19, 0, 0),
                CustomerName = "Bob Johnson",
                CustomerEmail = "bob.johnson@example.com",
                PerformanceId = "7"
            };
            builder.Entity<Booking>().HasData(booking1, booking2, booking3);
        }
    }
}