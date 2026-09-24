using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string Username { get; set; } = "";

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = "";
}