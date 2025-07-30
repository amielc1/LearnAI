
using Microsoft.Extensions.DependencyInjection;
using System;

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
        // GetRequiredService יזרוק שגיאה אם השירות לא רשום - וזה טוב.
        return Services!.GetRequiredService<T>();
    }
}