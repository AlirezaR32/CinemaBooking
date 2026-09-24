using CinemaBooking.Api.Data;
using CinemaBooking.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowTimesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ShowTimesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShowTime>>> GetShowTimes()
    {
        var showTimes = await _context.ShowTimes
            .Include(x => x.Movie)
            .Include(x => x.Cinema)
            .ToListAsync();

        return Ok(showTimes);
    }

    [HttpPost]
    public async Task<ActionResult<ShowTime>> CreateShowTime(ShowTime showTime)
    {
        var movie = await _context.Movies.FindAsync(showTime.MovieId);
        var cinema = await _context.Cinemas.FindAsync(showTime.CinemaId);

        if (movie == null || cinema == null)
        {
            return BadRequest("Invalid MovieId or CinemaId.");
        }

        _context.ShowTimes.Add(showTime);
        await _context.SaveChangesAsync();

        return Ok(showTime);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ShowTime>>> SearchShowTimes(string movieName)
    {
        var showTimes = await _context.ShowTimes
        .Include(x => x.Movie)
        .Include(x => x.Cinema)
        .Where(x => x.Movie!.Name.Contains(movieName))
        .ToListAsync();
        return Ok(showTimes);
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<List<ShowTime>>> GetUpcomingShowTimes()
    {
        var showTimes = await _context.ShowTimes
            .Include(x => x.Movie)
            .Include(x => x.Cinema)
            .Where(x => x.StartTime >= DateTime.Now)
            .OrderBy(x => x.StartTime)
            .ToListAsync();

        return Ok(showTimes);
    }

    [HttpGet("movie/{movieId}/has-showtime")]
    public async Task<ActionResult<bool>> HasShowTime(int movieId)
    {
        var exists = await _context.ShowTimes
            .AnyAsync(x => x.MovieId == movieId);

        return Ok(exists);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetShowTimeSummary()
    {
        var result = await _context.ShowTimes
            .Select(x => new
            {
                MovieName = x.Movie!.Name,
                CinemaName = x.Cinema!.Name,
                StartTime = x.StartTime
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("movie/{movieId}")]
    public async Task<ActionResult<List<ShowTime>>> GetShowTimesByMovie(int movieId)
    {
        var showTimes = await _context.ShowTimes
            .Include(x => x.Movie)
            .Include(x => x.Cinema)
            .Where(x => x.MovieId == movieId)
            .Where(x => x.StartTime >= DateTime.Now)
            .OrderBy(x => x.StartTime)
            .ToListAsync();

        return Ok(showTimes);
    }

    [HttpGet("cinema/{cinemaId}")]
    public async Task<ActionResult<List<ShowTime>>> GetShowTimesByCinema(int cinemaId)
    {
        var showTimes = await _context.ShowTimes
            .Include(x => x.Movie)
            .Include(x => x.Cinema)
            .Where(x =>
                x.CinemaId == cinemaId &&
                x.StartTime >= DateTime.Now)
            .OrderBy(x => x.StartTime)
            .ToListAsync();

        return Ok(showTimes);
    }

    [HttpGet("movie/{movieId}/first")]
    public async Task<ActionResult<ShowTime>> GetFirstShowTime(int movieId)
    {
        var showTime = await _context.ShowTimes
            .Where(x => x.MovieId == movieId)
            .OrderBy(x => x.StartTime)
            .FirstOrDefaultAsync();

        if (showTime == null)
        {
            return NotFound();
        }

        return Ok(showTime);
    }
    [HttpGet("movie/{movieId}/count")]
    public async Task<ActionResult<int>> GetShowTimeCount(int movieId)
    {
        var count = await _context.ShowTimes
            .CountAsync(x => x.MovieId == movieId);

        return Ok(count);
    }

    [HttpGet("cinema-counts")]
    public async Task<ActionResult<IEnumerable<object>>> GetCinemaShowTimeCounts()
    {
        var result = await _context.ShowTimes
            .GroupBy(x => x.CinemaId)
            .Select(g => new
            {
                CinemaId = g.Key,
                ShowTimeCount = g.Count()
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("cinema-counts/upcoming")]
    public async Task<IActionResult> GetUpcomingShowTimeCounts()
    {
        var result = await _context.ShowTimes
            .Where(x => x.StartTime >= DateTime.Now)
            .GroupBy(x => x.CinemaId)
            .Select(group => new
            {
                CinemaId = group.Key,
                ShowTimeCount = group.Count()
            })
            .OrderByDescending(x => x.ShowTimeCount)
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("movie-counts")]
    public async Task<IActionResult> GetMovieShowTimeCounts()
    {
        var result = await _context.ShowTimes
        .Where(x => x.StartTime >= DateTime.Now)
        .GroupBy(x => x.MovieId)
        .Select(group => new
        {
            MovieId = group.Key,
            ShowTimeCount = group.Count()
        })
        .OrderByDescending(x => x.ShowTimeCount)
        .ToListAsync();

        return Ok(result);
    }

    [HttpGet("{showTimeId}/available-seats")]
    public async Task<IActionResult> GetAvailableSeats(int showTimeId)
    {
        var showTime = await _context.ShowTimes
            .Include(x => x.Hall)
            .FirstOrDefaultAsync(x => x.Id == showTimeId);

        if (showTime == null)
        {
            return NotFound();
        }

        var bookedSeatIds = await _context.BookingSeats
            .Where(x => x.ShowTimeId == showTimeId)
            .Select(x => x.SeatId)
            .ToListAsync();

        var availableSeats = await _context.Seats
            .Where(x => x.HallId == showTime.HallId)
            .Where(x => !bookedSeatIds.Contains(x.Id))
            .OrderBy(x => x.Number)
            .ToListAsync();

        return Ok(availableSeats);
    }
}