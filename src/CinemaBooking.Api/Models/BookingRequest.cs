namespace CinemaBooking.Api.Models;

public class BookingRequest
{
    public int ShowTimeId { get; set; }

    public List<int> SeatIds { get; set; } = new();
}