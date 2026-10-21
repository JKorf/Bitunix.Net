using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Ticker info
/// </summary>
public record BitunixFuturesTicker
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>markPrice</c>"] Mark price
    /// </summary>
    [JsonPropertyName("markPrice")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>lastPrice</c>"] Last price
    /// </summary>
    [JsonPropertyName("lastPrice")]
    public decimal LastPrice { get; set; }
    /// <summary>
    /// ["<c>open</c>"] Open price 24h
    /// </summary>
    [JsonPropertyName("open")]
    public decimal OpenPrice { get; set; }
    /// <summary>
    /// ["<c>last</c>"] Quantity of the last trade
    /// </summary>
    [JsonPropertyName("last")]
    public decimal LastTradeQuantity { get; set; }
    /// <summary>
    /// ["<c>quoteVol</c>"] Volume in quote asset
    /// </summary>
    [JsonPropertyName("quoteVol")]
    public decimal QuoteVolume { get; set; }
    /// <summary>
    /// ["<c>baseVol</c>"] Volume in base asset
    /// </summary>
    [JsonPropertyName("baseVol")]
    public decimal BaseVolume { get; set; }
    /// <summary>
    /// ["<c>high</c>"] High price 24h
    /// </summary>
    [JsonPropertyName("high")]
    public decimal HighPrice { get; set; }
    /// <summary>
    /// ["<c>low</c>"] Low price 24h
    /// </summary>
    [JsonPropertyName("low")]
    public decimal LowPrice { get; set; }
}

