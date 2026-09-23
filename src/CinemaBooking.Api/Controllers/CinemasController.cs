using CinemaBooking.Api.Data;
using CinemaBooking.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CinemasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CinemasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cinema>>> GetCinemas()
    {
        var cinemas = await _context.Cinemas.ToListAsync();

        return Ok(cinemas);
    }

    [HttpPost]
    public async Task<ActionResult<Cinema>> CreateCinema(Cinema cinema)
    {
        _context.Cinemas.Add(cinema);

        await _context.SaveChangesAsync();

        return Ok(cinema);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cinema>> GetCinema(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);

        if (cinema == null)
        {
            return NotFound();
        }

        return Ok(cinema);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Cinema>> UpdateCinema(int id, Cinema cinema)
    {
        var existingCinema = await _context.Cinemas.FindAsync(id);
        if (existingCinema == null)
        {
            return NotFound();
        }

        existingCinema.Name = cinema.Name;

        await _context.SaveChangesAsync();

        return Ok(existingCinema);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCinema(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();

        return Ok();
    }
}