using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Kline data
/// </summary>
public record BitunixKline
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>open</c>"] Open price
    /// </summary>
    [JsonPropertyName("open")]
    public decimal OpenPrice { get; set; }
    /// <summary>
    /// ["<c>high</c>"] High price
    /// </summary>
    [JsonPropertyName("high")]
    public decimal HighPrice { get; set; }
    /// <summary>
    /// ["<c>low</c>"] Low price
    /// </summary>
    [JsonPropertyName("low")]
    public decimal LowPrice { get; set; }
    /// <summary>
    /// ["<c>close</c>"] Close price
    /// </summary>
    [JsonPropertyName("close")]
    public decimal ClosePrice { get; set; }
    /// <summary>
    /// ["<c>volume</c>"] Volume
    /// </summary>
    [JsonPropertyName("volume")]
    public decimal Volume { get; set; }
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime OpenTime { get; set; }
}

