using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitunix.Net.Objects.Internal
{
    internal record BitunixFuturesSocketParamRequest
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("args")]
        public Parameters[] Parameters { get; set; } = [];
    }

    internal record BitunixFuturesSocketRequest
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("args")]
        public BitunixFuturesChannel[] Parameters { get; set; } = [];
    }

    internal record BitunixFuturesChannel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
    }
}
