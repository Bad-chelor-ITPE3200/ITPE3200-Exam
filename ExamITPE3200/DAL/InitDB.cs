namespace ExamITPE3200.Models;

public class InitDB
{
    //here we can put filler data and load it to a database
    public static void seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        FastFlatDbContext context = scope.ServiceProvider.GetRequiredService<FastFlatDbContext>();
        if (!context.Bookings.Any())
        {
            
        }
    }
}