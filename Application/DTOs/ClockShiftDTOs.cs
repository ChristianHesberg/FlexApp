namespace Application.DTOs;

public class ClockInDTO
{
    public DateTime StartTime { get; set; }
    public int EmployeeId { get; set; }
}

public class ClockOutDTO
{
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }
}