using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FastFlat.DAL;

public class RentalDbContext : IdentityDbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
    {
    }

    //Creation for the different tables

    public DbSet<ListningModel>? Rentals { get; set; }

    public DbSet<AmenityModel>? ListningAmenities { get; set; }
    
    public DbSet<BookingModel>? Bookings { get; set; }

    public new DbSet<IdentityRole>? Roles { get; set; } // roles for the database
    
    public new DbSet<ApplicationUser>? Users { get; set; }

    public DbSet<AmenityModel>? Amenities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}