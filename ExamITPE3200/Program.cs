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
        builder.Configuration["ConnectionStrings:FastDbContextConnection"]);
});
builder.Services.AddDbContext<FastFlatDbContext>(Option => Option.UseSqlite("DbContextConnection"));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}
app.UseStaticFiles(); // for pictures
app.MapDefaultControllerRoute(); //to understand the MVC design with wwwroot
app.Run();