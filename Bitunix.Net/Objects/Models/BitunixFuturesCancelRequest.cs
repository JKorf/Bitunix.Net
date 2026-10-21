using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models
{
    /// <summary>
    /// Cancel request
    /// </summary>
    public record BitunixFuturesCancelRequest
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? OrderId { get; set; }

        /// <summary>
        /// ["<c>clientId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
    }
}
