using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class FlexBalanceRepository : IFlexBalanceRepository
{

    private AppDbContext _dbContext;
    
    public FlexBalanceRepository(AppDbContext context)
    {
        _dbContext = context;
    }


    public double GetFlexBalance(int userId)
    {
        return _dbContext.AppUsers.Select(u => u.FlexBalance).First();
    }

    public void AddFlexBalance(int userId, double amount)
    {
        AppUser user = _dbContext.AppUsers.Find(userId);
        if (user != null)
        {
            double balance = user.FlexBalance;
            user.FlexBalance = balance + amount;
            _dbContext.SaveChanges();
        }
        if(user==null)
            throw new KeyNotFoundException();
    }
}