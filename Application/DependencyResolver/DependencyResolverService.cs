using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IFlexBalanceService, FlexBalanceService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IClockShiftService, ClockShiftService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
}