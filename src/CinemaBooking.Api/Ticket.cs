public interface ITicketPrice
{
    double CaculatorPrice();
}

public class NormalTicket : ITicketPrice
{
    public double CaculatorPrice()
    {
        return 100;
    }
}

public class ChildTicket : ITicketPrice
{
    public double CaculatorPrice()
    {
        return 70;
    }
}


public class VipTicket : ITicketPrice
{
    public double CaculatorPrice()
    {
        return 200;
    }
}

