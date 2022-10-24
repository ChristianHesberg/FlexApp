using Application.DTOs;
using Application.Interfaces;
using Application.Util;
using Domain.Models;

namespace Application.Services;

public class FlexBalanceService : IFlexBalanceService
{
    private IFlexBalanceRepository _repo;

    public FlexBalanceService(IFlexBalanceRepository flexRepo)
    {
        _repo = flexRepo;
    }
    
    public double GetFlexBalance(int userId)
    {
        return _repo.GetFlexBalance(userId);
    }

    public void AddInitialFlexBalance(Session session, Schedule schedule)
    {
        double amount = CalculateShiftLength.CalculateLength(session.StartTime, session.EndTime);
        if (!schedule.Logged)
            amount -= schedule.ShiftLength;
        _repo.AddFlexBalance(session.EmployeeId, amount);
    }
    
    public void AddFlexBalance(Session session)
    {
        double amount = CalculateShiftLength.CalculateLength(session.StartTime, session.EndTime);

        _repo.AddFlexBalance(session.EmployeeId, amount);
    }

    public void RemoveFlexBalance(Session session)
    {
        double amount = CalculateShiftLength.CalculateLength(session.StartTime, session.EndTime);
        double newAmount = amount * -1;

        _repo.AddFlexBalance(session.EmployeeId, newAmount);
    }

    public void EditFlexBalance(Session editedSession, Session oldSession)
    {
        RemoveFlexBalance(oldSession);
        AddFlexBalance(editedSession);
    }

    public void EditFlexDirect(int userId, double amount)
    {
        _repo.AddFlexBalance(userId, amount);
    }
}