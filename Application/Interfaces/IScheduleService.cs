using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IScheduleService
{
    public List<Schedule> GetScheduleForEmployee(int employeeId);
    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end);
    public Schedule AddSchedule(AddScheduleDTO schedule);
    public Schedule EditSchedule(EditScheduleDTO schedule);
    public void DeleteSchedule(int id);
}