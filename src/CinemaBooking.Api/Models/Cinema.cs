namespace CinemaBooking.Api.Models;

public class Cinema
{
    public Cinema()
    {
    }
    public Cinema(string Name, List<Hall> Halls)
    {
        this.Name = Name;
        this.Halls = Halls;
    }
    public int Id { get; set; }
    public string Name { get; set; } ="";
    public List<Hall> Halls { get; set; }= new();


};

