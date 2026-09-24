namespace CinemaBooking.Api.Models;

public class BookingSeat
{
    public int Id { get; set; }

    public int ShowTimeId { get; set; }
    public ShowTime ShowTime { get; set; } = null!;

    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
}