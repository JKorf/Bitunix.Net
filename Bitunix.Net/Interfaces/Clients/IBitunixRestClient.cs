using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitunix.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitunix Rest API. 
    /// </summary>
    public interface IBitunixRestClient : IRestClient<BitunixCredentials>
    {
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IBitunixRestClientFuturesApi"/>
        public IBitunixRestClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBitunixRestClientSpotApi"/>
        public IBitunixRestClientSpotApi SpotApi { get; }

    }
}
