using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

List<int> ids = new List<int>();
ids.Add(1);
ids.Add(2);
ids.Add(3);

FlexBalanceRepository repo = new FlexBalanceRepository();
