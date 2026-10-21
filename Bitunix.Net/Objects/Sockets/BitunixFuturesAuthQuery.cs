using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;
using Bitunix.Net.Objects.Internal;
using CryptoExchange.Net.Clients;

namespace Bitunix.Net.Objects.Sockets
{
    internal class BitunixFuturesAuthQuery: Query<object>
    {
        private readonly SocketApiClient _client;

        public BitunixFuturesAuthQuery(SocketApiClient client, BitunixFuturesSocketParamRequest request) : base(request, false, 1)
        {
            _client = client;

            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateForQuery<object>("login", HandleMessage);
        }

        public CallResult<object> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, object message)
        {
            return CallResult.Ok(message, originalData);
        }
    }
}
