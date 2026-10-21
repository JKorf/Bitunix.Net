using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures streams
    /// </summary>
    public interface IBitunixSocketClientFuturesApi : ISocketApiClient<BitunixCredentials>, IDisposable
    {
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBitunixSocketClientFuturesSharedApi SharedApi { get; }

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Ticker%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<BitunixTickerUpdate>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Ticker%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/depth%20channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="limit">Order book depth, 1, 5 or 15 for full snapshot, or null for incremental updates</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<BitunixOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/depth%20channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="limit">Order book depth, 1, 5 or 15 for full snapshot, or null for incremental updates</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? limit, Action<DataEvent<BitunixOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/depth%20channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="priceType">Price type</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesSocketKlineInterval interval, PriceType? priceType, Action<DataEvent<BitunixKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/depth%20channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="priceType">Price type</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, FuturesSocketKlineInterval interval, PriceType? priceType, Action<DataEvent<BitunixKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price and funding updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/MarketPrice%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<BitunixMarkPriceUpdate>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to mark price and funding updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/MarketPrice%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixMarkPriceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Trade%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitunixTradeUpdate[]>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Trade%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Tickers%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<BitunixAggTickerUpdate[]>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://www.bitunix.com/api-docs/futures/websocket/public/Tickers%20Channel.html" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixAggTickerUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user data updates
        /// <para>
        /// <a href="https://www.bitunix.com/api-docs/futures/websocket/private/Balance%20Channel.html" /><br />
        /// <a href="https://www.bitunix.com/api-docs/futures/websocket/private/Order%20Channel.html" /><br />
        /// <a href="https://www.bitunix.com/api-docs/futures/websocket/private/Position%20Channel.html" /><br />
        /// <a href="https://www.bitunix.com/api-docs/futures/websocket/private/Tp%20Sl%20Channel.html" /><br /></para>
        /// </summary>
        /// <param name="onBalanceMessage">Event handler for balance updates</param>
        /// <param name="onOrderMessage">Event handler for order updates</param>
        /// <param name="onPositionMessage">Event handler for position updates</param>
        /// <param name="onTpSlMessage">Event handler for tp/sl order updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(
            Action<DataEvent<BitunixBalanceUpdate>>? onBalanceMessage = null,
            Action<DataEvent<BitunixFuturesOrderUpdate>>? onOrderMessage = null,
            Action<DataEvent<BitunixPositionUpdate>>? onPositionMessage = null,
            Action<DataEvent<BitunixTpSlUpdate>>? onTpSlMessage = null,
            CancellationToken ct = default);
    }
}
