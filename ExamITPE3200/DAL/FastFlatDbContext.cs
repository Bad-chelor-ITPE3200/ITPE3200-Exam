using System.Data;
using exam_personal.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamITPE3200.Models;

public class FastFlatDbContext : DbContext
{
    public FastFlatDbContext(DbContextOptions<FastFlatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Datasource=FastFlat.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<BookingModel> bookings { get; set; }
    public DbSet<CityModel> cities { get; set; }
    public DbSet<ContryModel> countries { get; set; }
    public DbSet<landlordModel> landlord { get; set; }
    public DbSet<RenterModel> renter { get; set; }
    public DbSet<ListingModel> listing { get; set; }
    public DbSet<UserModel> users { get; set; }
}