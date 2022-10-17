namespace Domain.Models;

public class AppUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double FlexBalance { get; set; }
    public double HoursPerDay { get; set; }
    public double DaysPerWeek { get; set; }
    public bool Active { get; set; }
    public ICollection<Session>? Sessions { get; set; }
}