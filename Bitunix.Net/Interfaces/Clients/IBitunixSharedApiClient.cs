using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;

namespace Bitunix.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Bitunix
    /// </summary>
    public interface IBitunixSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IBitunixRestClientSpotSharedApi SpotRest { get; }
        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IBitunixSocketClientSpotSharedApi SpotSocket { get; }
        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IBitunixRestClientFuturesSharedApi FuturesRest { get; }
        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IBitunixSocketClientFuturesSharedApi FuturesSocket { get; }
    }
}
