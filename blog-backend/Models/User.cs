namespace blog_backend.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? ProfilePicture { get; set; }
}