public class Movie
{
    private string _name = "";
    private TimeOnly _time;
    private DateTime _dateOfCreate;

    private Movie()
    {
    }
    public Movie(string name,TimeOnly time, DateTime dateOfCreate)
    {
        this._name = name;
        this._time = time;
        this._dateOfCreate = dateOfCreate;
    }
    public int Id { get; set; }
    
    public string Name { get
        {
            return this._name;
        }
        }
    public TimeOnly Time { get
        {
            return this._time;
        }
    }
    public DateTime DateOfCreate { get
        {
            return this._dateOfCreate;
        }
    }

    public void UpdateDetails(string? name,TimeOnly? time, DateTime? dateOfCreate)
    {
        if(name != null)
        {
            this._name = name;
        }

        if(time != null)
        {
            this._time = time.Value;
        }
        
        if(dateOfCreate != null)
        {
            this._dateOfCreate = dateOfCreate.Value;
        }
    }
    
}