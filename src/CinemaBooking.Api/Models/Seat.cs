using System.Diagnostics;

namespace CinemaBooking.Api.Models;

public class Seat
{
    public Seat(int number)
    {
        this.Number = number;
        this.IsBooked = false;
    }
    public int Number { get; set; }
    public bool IsBooked { get; set; }
}