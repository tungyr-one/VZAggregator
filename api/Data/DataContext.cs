using Microsoft.EntityFrameworkCore;
using VZAggregator.Models;

namespace VZAggregator.Data
{
    public class DataContext:DbContext
    {
        private int _idsStartValue = 7;
        //Server=localhost;Database=master;Trusted_Connection=True;

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Transport> Transports { get; set; }
        
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
            .Property(c => c.OrderId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired();
            
            modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders);

            modelBuilder.Entity<Order>()
            .Property(o => o.OrderPrice)
            .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<User>()
            .Property(c => c.UserId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired();

            modelBuilder.Entity<User>()
            .Property(o => o.Discount)
            .HasColumnType("decimal(18, 4)");  

            modelBuilder.Entity<Carrier>()
            .Property(c => c.CarrierId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired(); 

            modelBuilder.Entity<Carrier>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Carrier>(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction);
           

            modelBuilder.Entity<Address>()
            .Property(c => c.AddressId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired();   

            modelBuilder.Entity<Transport>()
            .Property(c => c.TransportId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired();  

            
            modelBuilder.Entity<Trip>()
            .Property(c => c.TripId)
            // .UseIdentityColumn(_idsStartValue)
            .IsRequired();

            modelBuilder.Entity<Trip>()
            .Property(o => o.TripPrice)
            .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Trip>()
            .HasMany(t => t.Orders)
            .WithOne(o => o.Trip)
            .HasForeignKey(c => c.TripId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trip>()
            .HasOne(t => t.DepartureAddress)
            .WithMany()
            .HasForeignKey(d => d.DepartureAddressId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trip>()
            .HasOne(t => t.DestinationAddress)
            .WithMany()
            .HasForeignKey(d => d.DestinationAddressId)
            .OnDelete(DeleteBehavior.NoAction);

            }
    }
}