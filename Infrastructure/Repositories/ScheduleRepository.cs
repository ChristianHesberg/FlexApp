using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    
    private AppDbContext _dbContext;

    public ScheduleRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public List<Schedule> GetScheduleForEmployee(int employeeId)
    {
        List<Schedule> schedules = _dbContext.Schedules.Where(s => s.EmployeeId == employeeId).Select(s => s).ToList();
        if (schedules != null)
            return schedules;
        throw new KeyNotFoundException();
    }

    public List<Schedule> GetScheduleForEmployeeInRange(int employeeId, DateTime start, DateTime end)
    {
        List<Schedule> schedules = _dbContext.Schedules
            .Where(s => s.EmployeeId == employeeId)
            .Where(s => s.StartTime.Date >= start.Date)
            .Where(s => s.StartTime.Date <= end.Date)
            .Select(s => s)
            .ToList();
        if (schedules != null)
            return schedules;
        throw new ArgumentException();
    }

    public Schedule AddSchedule(Schedule schedule)
    {
        _dbContext.Schedules.Add(schedule);
        _dbContext.SaveChanges();
        return schedule;
        /*return new Schedule()
        {
            Id = added.Id,
            StartTime = added.StartTime,
            EndTime = added.EndTime,
            ShiftLength = added.ShiftLength,
            EmployeeId = added.EmployeeId
        };
        */
    }

    public Schedule EditSchedule(Schedule schedule)
    {
        Schedule edit = _dbContext.Schedules.Find(schedule.Id);
        if (edit != null)
        {
            edit.StartTime = schedule.StartTime;
            edit.EndTime = schedule.EndTime;
            edit.ShiftLength = schedule.ShiftLength;
            _dbContext.SaveChanges();
            return schedule;
        }
        throw new KeyNotFoundException();
    }

    public void DeleteSchedule(int id)
    {
        Schedule schedule = new Schedule() { Id = id };
        _dbContext.Remove(schedule);
        _dbContext.SaveChanges();
    }
}