using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Api.Models;

public class BookingRequest
{
    [Range(1, int.MaxValue)]
    public int ShowTimeId { get; set; }

    [Required]
    [MinLength(1)]
    public List<int> SeatIds { get; set; } = new();
}