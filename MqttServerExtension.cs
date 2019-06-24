using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.AspNetCore;
using MQTTnet.Diagnostics;
using MQTTnet.Server;
using System;

namespace MQTTnet.AspNetCoreEx
{
    public static class MqttServerExtension
    {
        public static IApplicationBuilder UseMqttServerEx(this IApplicationBuilder app, Action<IMqttServerEx> configure)
        {
            var server = app.ApplicationServices.GetRequiredService<IMqttServerEx>();

            configure(server);

            return app;
        }

        public static IServiceCollection AddHostedMqttServerEx(this IServiceCollection services, IMqttServerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            services.AddSingleton(options);

            services.AddHostedMqttServerEx();

            return services;
        }

        public static IServiceCollection AddHostedMqttServerEx(this IServiceCollection services, Action<MqttServerOptionsBuilder> configure)
        {
            services.AddSingleton<IMqttServerOptions>(s =>
            {
                var builder = new MqttServerOptionsBuilder();
                configure(builder);
                return builder.Build();
            });

            services.AddHostedMqttServerEx();

            return services;
        }

        public static IServiceCollection AddHostedMqttServerExWithServices(this IServiceCollection services, Action<AspNetMqttServerOptionsBuilder> configure)
        {
            services.AddSingleton(s =>
            {
                var builder = new AspNetMqttServerOptionsBuilder(s);
                configure(builder);
                return builder.Build();
            });

            services.AddHostedMqttServerEx();

            return services;
        }

        public static IServiceCollection AddHostedMqttServerEx<TOptions>(this IServiceCollection services)
            where TOptions : class, IMqttServerOptions
        {
            services.AddSingleton<IMqttServerOptions, TOptions>();

            services.AddHostedMqttServerEx();

            return services;
        }

        private static IServiceCollection AddHostedMqttServerEx(this IServiceCollection services)
        {
            var logger = new MqttNetLogger();
            var childLogger = logger.CreateChildLogger();
            services.AddSingleton<IMqttNetLogger>(logger);
            services.AddSingleton(childLogger);
            services.AddSingleton<MqttHostedServerEx>();
            services.AddSingleton<IHostedService>(s => s.GetService<MqttHostedServerEx>());
            services.AddSingleton<IMqttServerEx>(s =>
            {
                var mhse = s.GetService<MqttHostedServerEx>();
                var store = s.GetService<IMqttServerStorage>();
                MqttServerOptions options = (MqttServerOptions)mhse.Options;
                options.ConnectionValidator = mhse.ConnectionValidator;
                if (options.Storage == null) options.Storage = store;
                return mhse;
            }
            );
            return services;
        }
    }
}