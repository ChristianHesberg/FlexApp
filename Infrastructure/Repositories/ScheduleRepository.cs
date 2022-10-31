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

    public Schedule GetScheduleForEmployeeAtDate(int employeeId, DateTime date)
    {
        Schedule schedule = _dbContext.Schedules
            .Where(s => s.EmployeeId == employeeId)
            .Where(s => s.StartTime.Date == date.Date)
            .Select(s => s)
            .FirstOrDefault();
        if (schedule != null)
            return schedule;
        return new Schedule() { EmployeeId = -1, Id = -1};
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

    public void LogSchedule(int id)
    {
        Schedule schedule = _dbContext.Schedules.Find(id);
        if (schedule != null)
        {
            schedule.Logged = true;
            _dbContext.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }

    public Schedule AddSchedule(Schedule schedule)
    {
        _dbContext.Schedules.Add(schedule);
        _dbContext.SaveChanges();
        return schedule;
    }

    public Schedule EditSchedule(Schedule schedule, out Schedule oldSchedule)
    {
        Schedule edit = _dbContext.Schedules.Find(schedule.Id);
        if (edit != null)
        {
            oldSchedule = new Schedule()
            {
                Id = edit.Id,
                EmployeeId = edit.EmployeeId,
                StartTime = edit.StartTime,
                EndTime = edit.EndTime,
                ShiftLength = edit.ShiftLength,
                Logged = edit.Logged
            }; 
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