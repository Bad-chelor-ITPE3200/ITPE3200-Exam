using System.Data;
using Microsoft.EntityFrameworkCore;
using ExamITPE3200.Models;

namespace ExamITPE3200.DAL;

public class FastFlatDbContext : DbContext
{
    public FastFlatDbContext(DbContextOptions<FastFlatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


    public DbSet<BookingModel> Bookings { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<ContryModel> Countries { get; set; }
    public DbSet<LandlordModel> Landlord { get; set; }
    public DbSet<RenterModel> Renters { get; set; }
    public DbSet<ListingModel> Listings { get; set; }
    public DbSet<UserModel> Users { get; set; }
   
}
