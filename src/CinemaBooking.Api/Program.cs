using CinemaBooking.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddOpenApi();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    Console.WriteLine($"Database: {db.Database.GetDbConnection().Database}");
    Console.WriteLine($"Server: {db.Database.GetDbConnection().DataSource}");
}

app.Run();

Movie movie = new Movie(
    "Interstellar",
    new TimeOnly(20, 30),
    DateTime.Now
);

Console.WriteLine(movie.Name);
Console.WriteLine(movie.Time);
Console.WriteLine(movie.DateOfCreate);


movie.UpdateDetails(
    "Interstellar 2",
    new TimeOnly(22, 00),
    null
);

Console.WriteLine(movie.Name);
Console.WriteLine(movie.Time);

Booking booking = new Booking("Alireza", "First Hall", 15, DateTime.Now);
BookingService bookingService = new BookingService();
bookingService.CreateBooking(booking);