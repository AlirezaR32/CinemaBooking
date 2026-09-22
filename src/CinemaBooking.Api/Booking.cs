public class Booking
{
    public Booking(string movieName, string hallName, int seatNumber, DateTime bookingDate)
    {
        MovieName = movieName;
        HallName = hallName;
        SeatNumber = seatNumber;
        BookingDate = bookingDate;
    }

    public string MovieName { get; set; }
    public string HallName { get; set; }
    public int SeatNumber { get; set; }
    public DateTime BookingDate { get; set; }
}