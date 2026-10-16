using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitunix.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitunix websocket API
    /// </summary>
    public interface IBitunixSocketClient : ISocketClient<BitunixCredentials>
    {
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IBitunixSocketClientFuturesApi"/>
        public IBitunixSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBitunixSocketClientSpotApi"/>
        public IBitunixSocketClientSpotApi SpotApi { get; }


    }
}
