using Domain.Models;

namespace Application.Interfaces;

public interface IFlexBalanceRepository
{
    public double GetFlexBalance(int userId);
    public void AddFlexBalance(int userId, double amount);
}