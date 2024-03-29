using FastFlat.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

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
    .File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

//filtrerer loggen
loggerConf.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
e.Level == LogEventLevel.Information && e.MessageTemplate.Text.Contains("Ececuted DbCommand"));

//creating logger
var logger = loggerConf.CreateLogger();
builder.Logging.AddSerilog(logger);


builder.Services.AddHttpClient();

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

builder.Services.AddDistributedMemoryCache();


builder.Services.AddScoped(typeof(IRentalRepository<>), typeof(RentalRepository<>));

builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".WeAreInSession.session";
    options.IdleTimeout = TimeSpan.FromMinutes(6);
    options.Cookie.IsEssential = true;
});

// Register DBInit as a transient service
builder.Services.AddTransient<DBInit>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Create an instance of DBInit and call Seed
    using var scope = app.Services.CreateScope(); 
    await DBInit.Seed(app); // This line calls the Seed method on the DBInit instance.
}

app.UseStaticFiles();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
