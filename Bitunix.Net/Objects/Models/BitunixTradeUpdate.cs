using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Trade update
/// </summary>
public record BitunixTradeUpdate
{
    /// <summary>
    /// ["<c>p</c>"] Price
    /// </summary>
    [JsonPropertyName("p")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>v</c>"] Quantity
    /// </summary>
    [JsonPropertyName("v")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>s</c>"] Side
    /// </summary>
    [JsonPropertyName("s")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>t</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("t")]
    public DateTime Timestamp { get; set; }
}

