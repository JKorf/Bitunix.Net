using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Sockets.Subscriptions
{
    internal record BitunixSocketConnectMessage
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
    }

    internal class BitunixConnectSubscription : SystemSubscription
    {
        public BitunixConnectSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateVoid<BitunixSocketConnectMessage>("connect");
        }
    }
}
