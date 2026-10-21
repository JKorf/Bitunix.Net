using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;
using Bitunix.Net.Objects.Internal;
using CryptoExchange.Net.Clients;

namespace Bitunix.Net.Objects.Sockets
{
    internal class BitunixFuturesQuery<T> : Query<T>
    {
        private readonly SocketApiClient _client;

        public BitunixFuturesQuery(SocketApiClient client, BitunixFuturesSocketParamRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;

            ExpectsResponse = false;

            MessageRouter = MessageRouter.CreateForQuery<object>("", HandleMessage);
        }

        public CallResult<object> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, object message)
        {
            return CallResult.Ok(message, originalData);
        }
    }
}
