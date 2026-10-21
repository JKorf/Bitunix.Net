using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;

namespace Bitunix.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitunixFuturesAuthSubscription : Subscription
    {
        private readonly Action<DataEvent<BitunixBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<BitunixFuturesOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BitunixPositionUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BitunixTpSlUpdate>>? _tpSlHandler;
        private readonly SocketApiClient _client;


        /// <summary>
        /// ctor
        /// </summary>
        public BitunixFuturesAuthSubscription(
            SocketApiClient client,
            ILogger logger, 
            Action<DataEvent<BitunixBalanceUpdate>>? balanceHandler,
            Action<DataEvent<BitunixFuturesOrderUpdate>>? orderHandler,
            Action<DataEvent<BitunixPositionUpdate>>? positionHandler,
            Action<DataEvent<BitunixTpSlUpdate>>? tpSlHandler
            ) : base(logger, true)
        {
            _client = client;
            _balanceHandler = balanceHandler;
            _orderHandler = orderHandler;
            _positionHandler = positionHandler;
            _tpSlHandler = tpSlHandler;

            MessageRouter = MessageRouter.Create(
                MessageRoute.CreateForEvent<BitunixFuturesSocketUpdate<BitunixBalanceUpdate>>("balance", DoHandleMessage),
                MessageRoute.CreateForEvent<BitunixFuturesSocketUpdate<BitunixFuturesOrderUpdate>>("order", DoHandleMessage),
                MessageRoute.CreateForEvent<BitunixFuturesSocketUpdate<BitunixPositionUpdate>>("position", DoHandleMessage),
                MessageRoute.CreateForEvent<BitunixFuturesSocketUpdate<BitunixTpSlUpdate>>("tpsl", DoHandleMessage)
            );
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixFuturesSocketUpdate<BitunixBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _balanceHandler?.Invoke(
                new DataEvent<BitunixBalanceUpdate>(BitunixExchange.Metadata.Id, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset())
            );
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixFuturesSocketUpdate<BitunixFuturesOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _orderHandler?.Invoke(
                new DataEvent<BitunixFuturesOrderUpdate>(BitunixExchange.Metadata.Id, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset())
            );
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixFuturesSocketUpdate<BitunixPositionUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _positionHandler?.Invoke(
                new DataEvent<BitunixPositionUpdate>(BitunixExchange.Metadata.Id, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset())
            );
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixFuturesSocketUpdate<BitunixTpSlUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _tpSlHandler?.Invoke(
                new DataEvent<BitunixTpSlUpdate>(BitunixExchange.Metadata.Id, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset())
            );
            return CallResult.Ok();
        }
    }
}
