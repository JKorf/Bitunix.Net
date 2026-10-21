using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;
using Bitunix.Net.Objects.Internal;
using CryptoExchange.Net.Clients;

namespace Bitunix.Net.Objects.Sockets
{
    internal class BitunixQuery<T> : Query<T>
    {
        private readonly SocketApiClient _client;

        public BitunixQuery(SocketApiClient client, BitunixSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;

            MessageRouter = MessageRouter.CreateForQuery<BitunixSocketResponse<T>, T>(request.Id, HandleMessage);
        }

        public CallResult<T> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixSocketResponse<T> message)
        {
            if (message.Code != "0")
                return CallResult.Fail<T>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message)), originalData);

            return CallResult.Ok(message.Data, originalData);
        }
    }
}
