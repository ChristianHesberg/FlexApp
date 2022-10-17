using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("server=10.176.111.31; database=FlexApp; user=CSe21B_6; password=Water=melon1; encrypt = false;"));

List<int> ids = new List<int>();
ids.Add(1);
ids.Add(2);
ids.Add(3);

FlexBalanceRepository flexBalanceRepository = new FlexBalanceRepository(AppDbContext context);

app.Run();