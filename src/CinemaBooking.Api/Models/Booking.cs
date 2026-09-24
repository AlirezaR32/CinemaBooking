namespace CinemaBooking.Api.Models;

public class Booking
{
    public int Id { get; set; }

    public int ShowTimeId { get; set; }
    public ShowTime ShowTime { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<BookingSeat> Seats { get; set; } = new();
}