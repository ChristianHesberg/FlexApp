namespace Domain.Models;

public class Session
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
    public AppUser? User { get; set; }
}