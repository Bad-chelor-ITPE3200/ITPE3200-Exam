using System.Data;
using exam_personal.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamITPE3200.Models;

public class FastFlatDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Datasource=FastFlat.db");
    }
    public FastFlatDbContext(DbContextOptions<FastFlatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

 
    
  

    public DbSet<BookingModel> Bookings { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<ContryModel> Countries { get; set; }
    public DbSet<landlordModel> Landlord { get; set; }
    public DbSet<RenterModel> Renters { get; set; }
    public DbSet<ListingModel> Listings { get; set; }
    public DbSet<UserModel> Users { get; set; }
}