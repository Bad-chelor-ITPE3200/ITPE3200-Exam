using Microsoft.EntityFrameworkCore;
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<RentalDbContext>();

builder.Services.AddScoped(typeof(IRentalRepository<>), typeof(RentalRepository<>));

builder.Services.AddRazorPages();

builder.Services.AddSession();

// Register DBInit as a transient service
builder.Services.AddTransient<DBInit>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Create an instance of DBInit and call Seed
    using var scope = app.Services.CreateScope();
    var dbInit = scope.ServiceProvider.GetRequiredService<DBInit>();
    dbInit.Seed(); // This line calls the Seed method on the DBInit instance.
}

app.UseStaticFiles();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
