using Concert.Data.Entity;
using Microsoft.EntityFrameworkCore;


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
            // User Tabell
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
            //Booking Tabell 
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Booking>().Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>().Property(b => b.Name)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Booking>().Property(b => b.Email)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Booking>().Property(b => b.UserId)
                .HasColumnType("nvarchar(36)")
                .IsRequired();


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .IsRequired();

            SeedData(modelBuilder);

        }
        private void SeedData(ModelBuilder builder)
        {
            // Skapar användare 
            var user1 = new User
            {
                ID = "1",
                Name = "John",
                Email = "John123@example.com",
                Password = "John123",

            };

            var user2 = new User
            {
                ID = "12",
                Name = "Bob Bengtsson",
                Email = "bob@example.com",
                Password = "anotherpassword"
            };

            var user3 = new User
            {
                ID = "123",
                Name = "Bob Bengtsson",
                Email = "bob@example.com",
                Password = "anotherpassword"
            };


            builder.Entity<User>().HasData(user1, user2, user3);

            var booking1 = new Booking
            {
                Id = "1",
                Name = "John",
                Email = "alice@example.com",
                UserId = "1"

            };
            var booking2 = new Booking
            {
                Id = "12",
                Name = "Chrilleeeee",
                Email = "Chrilleeeee@example.com",
                UserId = "12"

            };
            var booking3 = new Booking
            {
                Id = "123",
                Name = "Ronnieee",
                Email = "RonniePonnie@example.com",
                UserId = "123"

            };
            builder.Entity<Booking>().HasData(booking1, booking2, booking3);    
        }
    }
}
