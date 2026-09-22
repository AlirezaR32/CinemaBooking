using Microsoft.AspNetCore.SignalR;

namespace CinemaBooking.Api.Models;

public class Hall
{
    public Hall(string name, int seatCount, List<Seat> seats)
    {
        Name = name;
        SeatCount = seatCount;
        this.Seats = seats; 
    }
    public string Name { get; set; }
    public int SeatCount { get; set; }
    public List<Seat> Seats {get; set;}
    
}