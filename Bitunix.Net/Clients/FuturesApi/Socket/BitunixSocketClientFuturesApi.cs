using Bitunix.Net.Clients.MessageHandlers;
using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Options;
using Bitunix.Net.Objects.Sockets;
using Bitunix.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the Bitunix Futures websocket Api
    /// </summary>
    internal partial class BitunixSocketClientFuturesApi : SocketApiClient<BitunixEnvironment, BitunixAuthenticationProvider, BitunixCredentials>, IBitunixSocketClientFuturesApi
    {
        #region fields
        private readonly BitunixSocketClientFuturesSharedApi _sharedApi;

        protected override ErrorMapping ErrorMapping => BitunixErrors.Errors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal BitunixSocketClientFuturesApi(ILoggerFactory? loggerFactory, BitunixSocketOptions options) :
            base(loggerFactory, BitunixExchange.Metadata.Id, options.Environment.FuturesSocketClientAddress!, options, options.FuturesOptions)
        {
            _sharedApi = new BitunixSocketClientFuturesSharedApi(this);

            AddSystemSubscription(new BitunixConnectSubscription(_logger));

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(20),
                connection => new BitunixFuturesPingQuery(new BitunixPingRequest { Ping = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).Value }),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        /// <inheritdoc />
        public IBitunixSocketClientFuturesSharedApi SharedApi => _sharedApi;

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);
        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new BitunixSocketFuturesMessageHandler();

        /// <inheritdoc />
        protected override BitunixAuthenticationProvider CreateAuthenticationProvider(BitunixCredentials credentials)
            => new BitunixAuthenticationProvider(credentials);

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection, IMessageProcessor? processor) 
        {
            return Task.FromResult<Query?>(new BitunixFuturesAuthQuery(this, AuthenticationProvider!.GetLogin(this)));
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<BitunixTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitunixFuturesSubscription<BitunixTickerUpdate>(this, _logger, "ticker", symbols.ToArray(), onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<BitunixOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], limit, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? limit, Action<DataEvent<BitunixOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var topic = limit == null ? "depth_books" : "depth_book" + limit;
            var subscription = new BitunixFuturesSubscription<BitunixOrderBookUpdate>(this, _logger, topic, symbols.ToArray(), onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            string symbol,
            FuturesSocketKlineInterval interval,
            PriceType? priceType,
            Action<DataEvent<BitunixKlineUpdate>> onMessage,
            CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, priceType, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            IEnumerable<string> symbols,
            FuturesSocketKlineInterval interval, 
            PriceType? priceType,
            Action<DataEvent<BitunixKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var priceTypeStr = priceType == PriceType.MarkPrice ? "mark" : "market";
            var subscription = new BitunixFuturesSubscription<BitunixKlineUpdate>(this, _logger, $"{priceTypeStr}_kline_{EnumConverter.GetString(interval)}", symbols.ToArray(), onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(
            string symbol,
            Action<DataEvent<BitunixMarkPriceUpdate>> onMessage,
            CancellationToken ct = default)
            => SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(
            IEnumerable<string> symbols,
            Action<DataEvent<BitunixMarkPriceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitunixFuturesSubscription<BitunixMarkPriceUpdate>(this, _logger, "price", symbols.ToArray(), onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            string symbol,
            Action<DataEvent<BitunixTradeUpdate[]>> onMessage,
            CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            IEnumerable<string> symbols,
            Action<DataEvent<BitunixTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitunixFuturesSubscription<BitunixTradeUpdate[]>(this, _logger, "trade", symbols.ToArray(), onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<BitunixAggTickerUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToAggregatedTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitunixAggTickerUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitunixFuturesSubscription<BitunixAggTickerUpdate[]>(this, _logger, "tickers", symbols.ToArray(), onMessage, false, false);
            return await SubscribeAsync(BaseAddress.AppendPath("public") + "/", subscription, ct).ConfigureAwait(false);
        }

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(
            Action<DataEvent<BitunixBalanceUpdate>>? onBalanceMessage = null,
            Action<DataEvent<BitunixFuturesOrderUpdate>>? onOrderMessage = null,
            Action<DataEvent<BitunixPositionUpdate>>? onPositionMessage = null,
            Action<DataEvent<BitunixTpSlUpdate>>? onTpSlMessage = null,
            CancellationToken ct = default)
        {
            var subscription = new BitunixFuturesAuthSubscription(this, _logger,  onBalanceMessage, onOrderMessage, onPositionMessage, onTpSlMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("private") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BitunixExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
