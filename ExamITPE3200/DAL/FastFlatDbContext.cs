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

    public DbSet<BookingModel> bookings;
    public DbSet<CityModel> cities;
    public DbSet<ContryModel> countries;
    public DbSet<landlordModel> landlord;
    public DbSet<RenterModel> renter;
    public DbSet<ListingModel> listing;
    public DbSet<UserModel> users; 
}