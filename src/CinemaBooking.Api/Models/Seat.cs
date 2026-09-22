using System.Diagnostics;

namespace CinemaBooking.Api.Models;

public class Seat
{
    private Seat()
    {
    }
    public Seat(int number)
    {
        this.Number = number;
        this.IsBooked = false;
    }
    public int Id { get; set; }
    public int Number { get; set; }
    public bool IsBooked { get; set; }
}