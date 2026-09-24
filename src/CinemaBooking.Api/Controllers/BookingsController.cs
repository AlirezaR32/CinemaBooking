using CinemaBooking.Api.Data;
using CinemaBooking.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CinemaBooking.Api.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(BookingRequest request)
    {
        var showTime = await _context.ShowTimes
            .Include(x => x.Hall)
            .FirstOrDefaultAsync(x => x.Id == request.ShowTimeId);

        if (showTime == null)
        {
            return NotFound("ShowTime not found.");
        }

        var seats = await _context.Seats
            .Where(x =>
                x.HallId == showTime.HallId &&
                request.SeatIds.Contains(x.Id))
            .ToListAsync();

        if (seats.Count != request.SeatIds.Count)
        {
            return BadRequest("One or more seats are invalid.");
        }

        var bookedSeatIds = await _context.BookingSeats
            .Where(x => x.ShowTimeId == request.ShowTimeId)
            .Where(x => request.SeatIds.Contains(x.SeatId))
            .Select(x => x.SeatId)
            .ToListAsync();

        if (bookedSeatIds.Any())
        {
            return Conflict(new
            {
                Message = "One or more seats are already booked.",
                SeatIds = bookedSeatIds
            });
        }

        var booking = new Booking
        {
            ShowTimeId = request.ShowTimeId
        };

        _context.Bookings.Add(booking);

        foreach (var seat in seats)
        {
            booking.Seats.Add(new BookingSeat
            {
                ShowTimeId = request.ShowTimeId,
                SeatId = seat.Id
            });
        }

        await _context.SaveChangesAsync();

        return Ok(booking);
    }
}