﻿using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddMediatR(config =>
        //{
        //    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        //    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        //});

        return services;
    }
}