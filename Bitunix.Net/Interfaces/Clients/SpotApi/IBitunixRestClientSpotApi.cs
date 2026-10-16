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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IBitunixRestClientSpotApiShared SharedClient { get; }
    }
}
