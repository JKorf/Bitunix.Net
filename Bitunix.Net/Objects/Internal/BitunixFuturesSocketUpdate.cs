using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitunix.Net.Objects.Internal
{
    internal record BitunixFuturesSocketUpdate
    {
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal record BitunixFuturesSocketUpdate<T> : BitunixFuturesSocketUpdate
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
