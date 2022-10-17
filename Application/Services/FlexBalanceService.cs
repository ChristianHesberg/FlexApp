using Application.DTOs;
using Application.Interfaces;
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

    public void AddFlexBalance(Session session)
    {
        double amountHours = session.EndTime.Hour - session.StartTime.Hour;
        double amountMinutes = session.EndTime.Minute - session.StartTime.Minute;
        double amount = amountHours + (amountMinutes / 60);

        _repo.AddFlexBalance(session.EmployeeId, amount);
    }

    public void RemoveFlexBalance(Session session)
    {
        double amountHours = session.EndTime.Hour - session.StartTime.Hour;
        double amountMinutes = session.EndTime.Minute - session.StartTime.Minute;
        double amount = amountHours + (amountMinutes / 60);
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