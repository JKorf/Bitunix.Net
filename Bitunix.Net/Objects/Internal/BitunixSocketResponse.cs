using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Internal
{
    internal record BitunixSocketResponse<T>
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;

        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
