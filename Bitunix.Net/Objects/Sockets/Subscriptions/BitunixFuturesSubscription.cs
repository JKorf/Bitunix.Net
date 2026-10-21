using Bitunix.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Bitunix.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitunixFuturesSubscription<T> : Subscription
    {
        private readonly Action<DataEvent<T>> _handler;
        private readonly SocketApiClient _client;

        private readonly string _channel;
        private readonly string[]? _symbols;

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixFuturesSubscription(SocketApiClient client, ILogger logger, string channel, string[]? symbols, Action<DataEvent<T>> handler, bool auth, bool registerTopicFilters = true) : base(logger, auth)
        {
            _client = client;
            _handler = handler;

            _channel = channel;
            _symbols = symbols;

            if (registerTopicFilters && symbols != null)
                MessageRouter = MessageRouter.CreateForEvent<BitunixFuturesSocketUpdate<T>>(channel, symbols!, DoHandleMessage);
            else
                MessageRouter = MessageRouter.CreateForEvent<BitunixFuturesSocketUpdate<T>>(channel, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BitunixFuturesQuery<object>(_client, new Internal.BitunixFuturesSocketParamRequest
            {
                Operation = "subscribe",
                Parameters = GetQueryParameters(_channel, _symbols)
            }, Authenticated);
        }

        private static Parameters[] GetQueryParameters(string channel, string[]? symbols)
        {
            var result = new List<Parameters>();
            if (symbols?.Length > 0)
            {
                foreach(var symbol in symbols)
                {
                    var channelSub = new Parameters(BitunixExchange._parameterSerializationSettings);
                    channelSub.Add("ch", channel);
                    channelSub.Add("symbol", symbol);
                    result.Add(channelSub);
                }
            }
            else
            {
                var channelSub = new Parameters(BitunixExchange._parameterSerializationSettings);
                channelSub.Add("ch", channel);
                result.Add(channelSub);
            }
            return result.ToArray();
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BitunixFuturesQuery<object>(_client, new Internal.BitunixFuturesSocketParamRequest
            {
                Operation = "unsubscribe",
                Parameters = GetQueryParameters(_channel, _symbols)
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
