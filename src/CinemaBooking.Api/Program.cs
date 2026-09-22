Movie movie = new Movie(
    "Interstellar",
    new TimeOnly(20, 30),
    DateTime.Now
);

Console.WriteLine(movie.Name);
Console.WriteLine(movie.Time);
Console.WriteLine(movie.DateOfCreate);


movie.UpdateDetails(
    "Interstellar 2",
    new TimeOnly(22, 00),
    null
);

Console.WriteLine(movie.Name);
Console.WriteLine(movie.Time);