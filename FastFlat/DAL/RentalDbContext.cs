using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FastFlat.Models;


namespace FastFlat.DAL;

public class RentalDbContext : IdentityDbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<ListningModel> Rentals { get; set; }
  //  public DbSet<UserModel> Users { get; set; }
    public DbSet<BookingModel> Bookings { get; set; }

    //public DbSet<CityModel> Cities { get; set; }

    public DbSet<ContryModel> Countries { get; set; }

    public DbSet<LandlordModel> Landlord { get; set; }

    public DbSet<RenterModel> Renters { get; set; }

    public DbSet<AmenityModel> Amenities { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }


}