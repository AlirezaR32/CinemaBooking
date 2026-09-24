using CinemaBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<ShowTime> ShowTimes { get; set; }
    public DbSet<BookingSeat> BookingSeats { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShowTime>()
            .HasOne(x => x.Hall)
            .WithMany()
            .HasForeignKey(x => x.HallId)
            .OnDelete(DeleteBehavior.NoAction);
        
         modelBuilder.Entity<BookingSeat>()
        .HasOne(x => x.Booking)
        .WithMany(x => x.Seats)
        .HasForeignKey(x => x.BookingId)
        .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingSeat>()
        .HasIndex(x => new { x.ShowTimeId, x.SeatId })
        .IsUnique();
        modelBuilder.Entity<BookingSeat>()
        .HasIndex(x => new { x.ShowTimeId, x.SeatId })
        .IsUnique();
    }

}