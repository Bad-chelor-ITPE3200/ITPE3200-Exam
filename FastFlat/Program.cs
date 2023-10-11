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
    .File($"Logs/appLog.txt");

//creating logger
var logger = loggerConf.CreateLogger();
builder.Logging.AddSerilog(logger);
//builder.Services.AddScoped<SignInManager<IdentityUser>>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password Settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 6;

        // Lockout Settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User Settings
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<RentalDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddScoped(typeof(IRentalRepository<>), typeof(RentalRepository<>));

builder.Services.AddRazorPages();

builder.Services.AddSession();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseSession();


app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();