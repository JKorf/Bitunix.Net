using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures streams
    /// </summary>
    public interface IBitunixSocketClientFuturesApi : ISocketApiClient<BitunixCredentials>, IDisposable
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<BitunixModel>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IBitunixSocketClientFuturesApiShared SharedClient { get; }
    }
}
