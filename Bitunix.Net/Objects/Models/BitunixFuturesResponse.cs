using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models
{
    internal record BitunixFuturesResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }

    internal record BitunixFuturesResponse<T> : BitunixFuturesResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
