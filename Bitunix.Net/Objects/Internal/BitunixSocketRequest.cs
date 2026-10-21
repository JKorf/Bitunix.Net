using CryptoExchange.Net.Objects;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Internal
{
    internal record BitunixSocketRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        [JsonPropertyName("params")]
        public Parameters Params { get; set; } = default!;
    }
}
