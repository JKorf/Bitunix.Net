using Bitunix.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Bitunix.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitunixFuturesSubscription<T> : Subscription
    {
        private readonly Action<DataEvent<T>> _handler;
        private readonly SocketApiClient _client;

        private readonly string _channel;
        private readonly string[] _symbols;

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixFuturesSubscription(SocketApiClient client, ILogger logger, string channel, string[] symbols, Action<DataEvent<T>> handler, bool auth, bool registerTopicFilters = true) : base(logger, auth)
        {
            _client = client;
            _handler = handler;

            _channel = channel;
            _symbols = symbols;

            if (registerTopicFilters)
                MessageRouter = MessageRouter.CreateForEvent<BitunixFuturesSocketUpdate<T>>(channel, symbols, DoHandleMessage);
            else
                MessageRouter = MessageRouter.CreateForEvent<BitunixFuturesSocketUpdate<T>>(channel, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BitunixFuturesQuery<object>(_client, new Internal.BitunixFuturesSocketRequest
            {
                Operation = "subscribe",
                Parameters = _symbols.Select(x => new BitunixFuturesChannel
                {
                    Channel = _channel,
                    Symbol = x
                }).ToArray()
            }, Authenticated);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BitunixFuturesQuery<object>(_client, new Internal.BitunixFuturesSocketRequest
            {
                Operation = "unsubscribe",
                Parameters = _symbols.Select(x => new BitunixFuturesChannel
                {
                    Channel = _channel,
                    Symbol = x
                }).ToArray()
            }, Authenticated);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixFuturesSocketUpdate<T> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _handler.Invoke(
                new DataEvent<T>(BitunixExchange.Metadata.Id, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset())
            );
            return CallResult.Ok();
        }
    }
}
