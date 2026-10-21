using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Sockets
{
    internal class BitunixPingRequest
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = "ping";
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
    }
    internal class BitunixPingResponse
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = "ping";
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
        [JsonPropertyName("pong")]
        public long Pong { get; set; }
    }

    internal class BitunixFuturesPingQuery : Query<BitunixPingResponse>
    {
        public BitunixFuturesPingQuery(BitunixPingRequest request) : base(request, false, 1)
        {
            MessageRouter = MessageRouter.CreateForQuery<BitunixPingResponse>(request.Ping.ToString(), HandleMessage);
        }

        public CallResult<BitunixPingResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitunixPingResponse message)
        {
            return CallResult.Ok(message, originalData);
        }
    }
}
