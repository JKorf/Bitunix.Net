using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models
{
    /// <summary>
    /// Order cancel request
    /// </summary>
    public record BitunixCancelOrderRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
