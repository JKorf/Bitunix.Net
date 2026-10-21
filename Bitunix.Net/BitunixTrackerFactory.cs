using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Bitunix.Net.Clients;
using Bitunix.Net.Interfaces;
using Bitunix.Net.Interfaces.Clients;

namespace Bitunix.Net
{
    /// <inheritdoc />
    public class BitunixTrackerFactory : IBitunixTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitunixTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval) => false;

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => false;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null, ExchangeParameters? exchangeParameters = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitunixRestClient>() ?? new BitunixRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitunixSocketClient>() ?? new BitunixSocketClient();

            throw new NotImplementedException();

            //IGetKlinesRest sharedRestClient;
            //ISubscribeKlinesSocket sharedSocketClient;
            //if (symbol.TradingMode == TradingMode.Spot)
            //{
            //    sharedRestClient = restClient.SpotApi.SharedApi;
            //}
            //else
            //{
            //    sharedRestClient = restClient.FuturesApi.SharedApi;
            //    sharedSocketClient = socketClient.FuturesApi.SharedApi;
            //}

            //return new KlineTracker(
            //    _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
            //    sharedRestClient,
            //    sharedSocketClient,
            //    symbol,
            //    interval,
            //    limit,
            //    period,
            //    exchangeParameters
            //    );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null, ExchangeParameters? exchangeParameters = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitunixRestClient>() ?? new BitunixRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitunixSocketClient>() ?? new BitunixSocketClient();

            throw new NotImplementedException();

            //IRecentTradeRestClient? sharedRestClient;
            //ITradeSocketClient sharedSocketClient;
            //if (symbol.TradingMode == TradingMode.Spot)
            //{
            //    sharedRestClient = restClient.SpotApi.SharedClient;
            //    sharedSocketClient = socketClient.SpotApi.SharedClient;
            //}
            //else
            //{
            //    sharedRestClient = restClient.FuturesApi.SharedClient;
            //    sharedSocketClient = socketClient.FuturesApi.SharedClient;
            //}

            //return new TradeTracker(
            //    _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
            //    sharedRestClient,
            //    null,
            //    sharedSocketClient,
            //    symbol,
            //    limit,
            //    period,
            //#warning check
            //    TradeQuantityType.BaseAsset,
            //    exchangeParameters
            //    );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null, ExchangeParameters? exchangeParameters = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitunixRestClient>() ?? new BitunixRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitunixSocketClient>() ?? new BitunixSocketClient();
            return new BitunixUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitunixUserSpotDataTracker>>() ?? new NullLogger<BitunixUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config,
                exchangeParameters
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, BitunixCredentials credentials, SpotUserDataTrackerConfig? config = null, BitunixEnvironment? environment = null, ExchangeParameters? exchangeParameters = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitunixUserClientProvider>() ?? new BitunixUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BitunixUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitunixUserSpotDataTracker>>() ?? new NullLogger<BitunixUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config,
                exchangeParameters
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null, ExchangeParameters? exchangeParameters = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitunixRestClient>() ?? new BitunixRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitunixSocketClient>() ?? new BitunixSocketClient();
            return new BitunixUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitunixUserFuturesDataTracker>>() ?? new NullLogger<BitunixUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config,
                exchangeParameters
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, BitunixCredentials credentials, FuturesUserDataTrackerConfig? config = null, BitunixEnvironment? environment = null, ExchangeParameters? exchangeParameters = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitunixUserClientProvider>() ?? new BitunixUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BitunixUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitunixUserFuturesDataTracker>>() ?? new NullLogger<BitunixUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config,
                exchangeParameters
                );
        }
    }
}
