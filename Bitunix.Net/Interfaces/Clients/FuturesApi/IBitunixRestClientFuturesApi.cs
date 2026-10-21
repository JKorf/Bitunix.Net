using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures API endpoints
    /// </summary>
    public interface IBitunixRestClientFuturesApi : IRestApiClient<BitunixCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBitunixRestClientFuturesApiAccount" />
        public IBitunixRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitunixRestClientFuturesApiExchangeData" />
        public IBitunixRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBitunixRestClientFuturesApiTrading" />
        public IBitunixRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBitunixRestClientFuturesSharedApi SharedApi { get; }

    }
}
