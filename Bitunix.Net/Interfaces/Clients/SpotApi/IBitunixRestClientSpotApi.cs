using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot API endpoints
    /// </summary>
    public interface IBitunixRestClientSpotApi : IRestApiClient<BitunixCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBitunixRestClientSpotApiAccount" />
        public IBitunixRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitunixRestClientSpotApiExchangeData" />
        public IBitunixRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBitunixRestClientSpotApiTrading" />
        public IBitunixRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBitunixRestClientSpotSharedApi SharedApi { get; }
    }
}
