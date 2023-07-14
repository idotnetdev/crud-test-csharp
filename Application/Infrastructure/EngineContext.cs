using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;
public static class EngineContext
{
    private static IServiceProvider _serviceProvider;

    public static void Initialize(IServiceCollection services)
    {
        _serviceProvider = services.BuildServiceProvider();
    }

    public static T Resolve<T>() => _serviceProvider.GetService<T>();
}
