
using Microsoft.Extensions.DependencyInjection; 

namespace AI.Core;
public static class ServiceLocator
{
    public static IServiceProvider? Services { get; private set; }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        Services = serviceProvider;
    }

    public static T GetService<T>() where T : notnull
    { 
        return Services!.GetRequiredService<T>();
    }
}
