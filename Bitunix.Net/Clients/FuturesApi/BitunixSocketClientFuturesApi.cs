using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Clients.MessageHandlers;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Options;
using Bitunix.Net.Objects.Sockets.Subscriptions;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the Bitunix Futures websocket Api
    /// </summary>
    internal partial class BitunixSocketClientFuturesApi : SocketApiClient<BitunixEnvironment, BitunixAuthenticationProvider, BitunixCredentials>, IBitunixSocketClientFuturesApi
    {
        #region fields
        protected override ErrorMapping ErrorMapping => BitunixErrors.Errors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        #warning address
        internal BitunixSocketClientFuturesApi(ILoggerFactory? loggerFactory, BitunixSocketOptions options) :
            base(loggerFactory, BitunixExchange.Metadata.Id, options.Environment.SpotSocketClientAddress!, options, options.FuturesOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);
        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new BitunixSocketSpotMessageHandler();

        /// <inheritdoc />
        protected override BitunixAuthenticationProvider CreateAuthenticationProvider(BitunixCredentials credentials)
            => new BitunixAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<BitunixModel>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitunixModel>((receiveTime, originalData, data) =>
            {
                //UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<BitunixModel>(BitunixExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        //.WithStreamId(data.Stream)
                        //.WithSymbol(data.Symbol)
                        //.WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new BitunixSubscription<BitunixModel>(_logger, new [] { "XXX" }, internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IBitunixSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BitunixExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
