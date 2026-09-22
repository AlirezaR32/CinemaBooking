using Microsoft.AspNetCore.SignalR;

namespace CinemaBooking.Api.Models;

public class Hall
{
    private Hall()
    {
        
    }
    public Hall(string name, int seatCount, List<Seat> seats)
    {
        Name = name;
        SeatCount = seatCount;
        this.Seats = seats; 
    }
    public int Id { get; set; }
    public string Name { get; set; }= "";
    public int SeatCount { get; set; } = 0;
    public List<Seat> Seats {get; set;}= new();
    
}