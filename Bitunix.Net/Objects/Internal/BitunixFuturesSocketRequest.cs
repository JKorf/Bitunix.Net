using CryptoExchange.Net.Objects;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Internal
{
    internal record BitunixFuturesSocketParamRequest
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("args")]
        public Parameters[] Parameters { get; set; } = [];
    }
}
