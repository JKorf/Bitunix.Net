using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Kline update
/// </summary>
public record BitunixKlineUpdate
{
    /// <summary>
    /// ["<c>o</c>"] Open price
    /// </summary>
    [JsonPropertyName("o")]
    public decimal OpenPrice { get; set; }
    /// <summary>
    /// ["<c>c</c>"] Close price
    /// </summary>
    [JsonPropertyName("c")]
    public decimal ClosePrice { get; set; }
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
    /// ["<c>b</c>"] Base asset volume
    /// </summary>
    [JsonPropertyName("b")]
    public decimal BaseVolume { get; set; }
    /// <summary>
    /// ["<c>q</c>"] Quote asset volume
    /// </summary>
    [JsonPropertyName("q")]
    public decimal QuoteVolume { get; set; }
}

