using ExamITPE3200.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Fixing a db 
builder.Services.AddDbContext<FastFlatDbContext>(Options =>
{
    Options.UseSqlite(
        builder.Configuration["ConnectionStrings:FastFlatDbContextConnection"]);
});
builder.Services.AddDbContext<FastFlatDbContext>(Option => Option.UseSqlite("DbContextConnection"));
var app = builder.Build();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();