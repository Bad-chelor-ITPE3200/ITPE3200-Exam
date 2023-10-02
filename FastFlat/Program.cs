using Microsoft.EntityFrameworkCore;
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Core;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RentalDbContextConnection") ?? throw new 
    InvalidOperationException("Connection string 'RentalDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<RentalDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:RentalDbContextConnection"]);
});
//configure logger
var loggerConf = new LoggerConfiguration().MinimumLevel.Information().WriteTo
    .File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}"); 

//creating logger
var logger = loggerConf.CreateLogger();
builder.Logging.AddSerilog(logger); 

builder.Services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<RentalDbContext>().AddDefaultUI();

builder.Services.AddScoped(typeof(IRentalRepository<>), typeof(RentalRepository<>));

builder.Services.AddRazorPages();

builder.Services.AddSession();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}



app.UseStaticFiles();

app.UseSession();
app.UseAuthorization();
app.UseAuthentication();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();