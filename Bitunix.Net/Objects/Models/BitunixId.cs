using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Id
/// </summary>
public record BitunixId
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>clientId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientOrderId { get; set; }
}

