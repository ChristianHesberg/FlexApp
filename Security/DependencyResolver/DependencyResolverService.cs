using Microsoft.Extensions.DependencyInjection;
using Security.Authentication;

namespace Security.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterSecurityLayer(IServiceCollection services)
    {
        services.AddScoped<IUserAuthenticator, UserAuthenticator>();
    }
}