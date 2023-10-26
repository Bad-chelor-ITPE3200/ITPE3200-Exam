using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FastFlat.DAL;

public class RentalDbContext : IdentityDbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    //public DbSet<ApplicationUser> AspNetUsers { get; set; }

    public DbSet<ListningModel> Rentals { get; set; }

    public DbSet<AmenityModel> ListningAmenities { get; set; }

    //public DbSet<AspNetUsers> Users { get; set; }

    //public DbSet<UserModel> Users { get; set; }
    public DbSet<BookingModel> Bookings { get; set; }

    public DbSet<IdentityRole> Roles { get; set; } // roles for the database

    //public DbSet<CityModel> Cities { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    public DbSet<AmenityModel> Amenities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}