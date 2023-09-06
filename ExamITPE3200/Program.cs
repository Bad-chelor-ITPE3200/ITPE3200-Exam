using ExamITPE3200.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Fixing a db 
/*builder.Services.AddDbContext<FastFlatDbContext>(Options =>
{
    Options.UseSqlite(
        builder.Configuration["ConnectionStrings:FastFlatDbContextConnection"]);
});*/
builder.Services.AddDbContext<FastFlatDbContext>(Option => Option.UseSqlite("Datasource=FastFlat.db"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();