using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IFlexBalanceService
{
    public double GetFlexBalance(int userId);
    public void AddInitialFlexBalance(Session session, Schedule schedule);

    public void AddFlexBalance(Session session);
    public void RemoveFlexBalance(Session session);

    public void EditFlexBalance(Session editedSession, Session oldSession);
    public void EditFlexDirect(int userId, double amount);
}