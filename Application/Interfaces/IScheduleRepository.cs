using Domain.Models;

namespace Application.Interfaces;

public interface IScheduleRepository
{
    public List<Schedule> GetScheduleForEmployee(int employeeId);
    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end);
    public Schedule AddSchedule(Schedule schedule);
    public Schedule EditSchedule(Schedule schedule);
    public void DeleteSchedule(int id);
}