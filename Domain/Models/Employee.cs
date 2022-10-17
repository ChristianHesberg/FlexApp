namespace Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double FlexBalance { get; set; }
    public ICollection<Session>? Sessions { get; set; }
    public ICollection<Schedule> ScheduledDays { get; set; }
}