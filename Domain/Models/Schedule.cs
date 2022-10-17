namespace Domain.Models;

public class Schedule
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double ShiftLength { get; set; }
    public Employee? Employee { get; set; }
}