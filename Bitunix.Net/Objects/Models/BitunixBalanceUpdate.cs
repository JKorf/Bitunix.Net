using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models
{
    /// <summary>
    /// Balance update
    /// </summary>
    public record BitunixBalanceUpdate
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>isolationFrozen</c>"] Isolation Frozen
        /// </summary>
        [JsonPropertyName("isolationFrozen")]
        public decimal IsolationFrozen { get; set; }
        /// <summary>
        /// ["<c>crossFrozen</c>"] Cross Frozen
        /// </summary>
        [JsonPropertyName("crossFrozen")]
        public decimal CrossFrozen { get; set; }
        /// <summary>
        /// ["<c>margin</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>isolationMargin</c>"] Isolation Margin
        /// </summary>
        [JsonPropertyName("isolationMargin")]
        public decimal IsolationMargin { get; set; }
        /// <summary>
        /// ["<c>crossMargin</c>"] Cross Margin
        /// </summary>
        [JsonPropertyName("crossMargin")]
        public decimal CrossMargin { get; set; }
        /// <summary>
        /// ["<c>expMoney</c>"] Exp money
        /// </summary>
        [JsonPropertyName("expMoney")]
        public decimal ExpMoney { get; set; }
    }
}
