﻿namespace Domain.Models;

public class Session
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}