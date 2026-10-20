using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models
{
    internal record BitunixResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

    internal record BitunixResponse<T> : BitunixResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
