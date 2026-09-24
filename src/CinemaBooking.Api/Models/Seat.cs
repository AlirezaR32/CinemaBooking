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
    }
    public int Id { get; set; }
    public int Number { get; set; }

    public int HallId { get; set; }

    public Hall Hall { get; set; } = null!;
}