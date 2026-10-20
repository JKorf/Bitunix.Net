using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// 
/// </summary>
public record BitunixFuturesKline
{
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
    /// ["<c>close</c>"] Close price
    /// </summary>
    [JsonPropertyName("close")]
    public decimal ClosePrice { get; set; }
    /// <summary>
    /// ["<c>low</c>"] Low price
    /// </summary>
    [JsonPropertyName("low")]
    public decimal LowPrice { get; set; }
    /// <summary>
    /// ["<c>time</c>"] Open time
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime OpenTime { get; set; }
    /// <summary>
    /// ["<c>quoteVol</c>"] Volume in quote asset
    /// </summary>
    [JsonPropertyName("quoteVol")]
    public decimal VolumeQuote { get; set; }
    /// <summary>
    /// ["<c>baseVol</c>"] Volume in base asset
    /// </summary>
    [JsonPropertyName("baseVol")]
    public decimal VolumeBase { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public PriceType Type { get; set; }
}

