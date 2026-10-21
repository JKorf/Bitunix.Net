using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
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
using CryptoExchange.Net.SharedApis;

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
            var options = BitunixOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

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
            var options = BitunixOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

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

            services.RegisterSharedApiClient<
                IBitunixSharedApiClient,
                BitunixSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.FuturesRest)
                    .Add(client => client.FuturesSocket)
                    );

            return services;
        }
    }
}
