namespace CinemaBooking.Api.Models;

public class Cinema
{
    public Cinema(string Name, List<Hall> Halls)
    {
        this.Name = Name;
        this.Halls = Halls;
    }
    public string Name { get; set; } ="";
    public List<Hall> Halls { get; set; }= new();


};

