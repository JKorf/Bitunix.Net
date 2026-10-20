using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Ticker update
/// </summary>
public record BitunixAggTickerUpdate
{
    /// <summary>
    /// ["<c>s</c>"] Symbol
    /// </summary>
    [JsonPropertyName("s")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>la</c>"] Last price
    /// </summary>
    [JsonPropertyName("la")]
    public decimal LastPrice { get; set; }
    /// <summary>
    /// ["<c>o</c>"] Open price
    /// </summary>
    [JsonPropertyName("o")]
    public decimal OpenPrice { get; set; }
    /// <summary>
    /// ["<c>h</c>"] High price
    /// </summary>
    [JsonPropertyName("h")]
    public decimal HighPrice { get; set; }
    /// <summary>
    /// ["<c>l</c>"] Low price
    /// </summary>
    [JsonPropertyName("l")]
    public decimal LowPrice { get; set; }
    /// <summary>
    /// ["<c>b</c>"] Volume in base asset
    /// </summary>
    [JsonPropertyName("b")]
    public decimal VolumeBase { get; set; }
    /// <summary>
    /// ["<c>q</c>"] Volume in quote asset
    /// </summary>
    [JsonPropertyName("q")]
    public decimal VolumeQuote { get; set; }
    /// <summary>
    /// ["<c>r</c>"] Price change percentage
    /// </summary>
    [JsonPropertyName("r")]
    public decimal PriceChangePercentage { get; set; }

    /// <summary>
    /// ["<c>bd</c>"] Best bid price
    /// </summary>
    [JsonPropertyName("bd")]
    public decimal BestBidPrice { get; set; }
    /// <summary>
    /// ["<c>ak</c>"] Best ask price
    /// </summary>
    [JsonPropertyName("ak")]
    public decimal BestAskPrice { get; set; }
    /// <summary>
    /// ["<c>bv</c>"] Best bid quantity
    /// </summary>
    [JsonPropertyName("bv")]
    public decimal BestBidQuantity { get; set; }
    /// <summary>
    /// ["<c>av</c>"] Best ask quantity
    /// </summary>
    [JsonPropertyName("av")]
    public decimal BestAskQuantity { get; set; }
}

