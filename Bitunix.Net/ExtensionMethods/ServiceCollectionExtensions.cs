using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Interfaces;
using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;
using Bitunix.Net.SymbolOrderBooks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IBitunixRestClient and IBitunixSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBitunix(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new BitunixOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid configuration provided", ex);
            }

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? BitunixEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? BitunixEnvironment.Live.Name;
            options.Rest.Environment = BitunixEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = BitunixEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddBitunixCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IBitunixRestClient and IBitunixSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Bitunix services</param>
        /// <returns></returns>
        public static IServiceCollection AddBitunix(
            this IServiceCollection services,
            Action<BitunixOptions>? optionsDelegate = null)
        {
            var options = new BitunixOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? BitunixEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? BitunixEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddBitunixCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBitunixCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBitunixRestClient, BitunixRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BitunixRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new BitunixRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<BitunixRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<BitunixRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IBitunixSocketClient), x => { return new BitunixSocketClient(x.GetRequiredService<IOptions<BitunixSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IBitunixOrderBookFactory, BitunixOrderBookFactory>();
            services.AddTransient<ITrackerFactory, BitunixTrackerFactory>();
            services.AddTransient<IBitunixTrackerFactory, BitunixTrackerFactory>();
            services.AddSingleton<IBitunixUserClientProvider, BitunixUserClientProvider>(x =>
                new BitunixUserClientProvider(
                    x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IBitunixRestClient).Name),
                    x.GetRequiredService<ILoggerFactory>(),
                    x.GetRequiredService<IOptions<BitunixRestOptions>>(),
                    x.GetRequiredService<IOptions<BitunixSocketOptions>>()));

#warning shared

            return services;
        }
    }
}
