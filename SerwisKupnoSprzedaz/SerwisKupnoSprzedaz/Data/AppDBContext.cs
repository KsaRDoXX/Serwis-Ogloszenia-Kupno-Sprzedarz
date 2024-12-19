using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Models;
using SerwisKupnoSprzedaz.Services;


namespace SerwisKupnoSprzedaz.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Announcment> Announcments { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseMySql("YourConnectionStringHere", new MySqlServerVersion(new Version(8, 0, 30))); // Pomelo MySQL
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja User -> Orders (1:N)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Order -> Announcement (1:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Announcement)
                .WithOne(a => a.Order)
                .HasForeignKey<Order>(o => o.AnnouncementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja User -> Announcements (1:N)
            modelBuilder.Entity<Announcment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Announcments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //others ? 

    }
}
