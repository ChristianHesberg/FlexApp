using Domain.Models;

namespace Application.Interfaces;

public interface IScheduleRepository
{
    public List<Schedule> GetScheduleForEmployee(int employeeId);
    public Schedule GetScheduleForEmployeeAtDate(int employeeId, DateTime date);
    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end);
    public Schedule AddSchedule(Schedule schedule);
    public Schedule EditSchedule(Schedule schedule, out Schedule oldSchedule);
    public void DeleteSchedule(int id);
}