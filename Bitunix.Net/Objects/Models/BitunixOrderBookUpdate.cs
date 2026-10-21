using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Order book update
/// </summary>
public record BitunixOrderBookUpdate
{
    /// <summary>
    /// ["<c>b</c>"] Price change percentage
    /// </summary>
    [JsonPropertyName("b")]
    public BitunixFuturesOrderBookEntry[] Bids { get; set; } = [];
    /// <summary>
    /// ["<c>a</c>"] Price change percentage
    /// </summary>
    [JsonPropertyName("a")]
    public BitunixFuturesOrderBookEntry[] Asks { get; set; } = [];
}

