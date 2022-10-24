using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IScheduleService
{
    public List<Schedule> GetScheduleForEmployee(int employeeId);
    public Schedule GetScheduleForEmployeeAtDate(int employeeId, DateTime date);
    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end);
    public void LogSchedule(int id);
    public Schedule AddSchedule(AddScheduleDTO schedule);
    public Schedule EditSchedule(EditScheduleDTO schedule, out Schedule oldSchedule);
    public void DeleteSchedule(int id);
}