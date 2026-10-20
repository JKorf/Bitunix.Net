using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Trading settings
/// </summary>
public record BitunixTradingSetting
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>marginCoin</c>"] Margin asset
    /// </summary>
    [JsonPropertyName("marginCoin")]
    public string MarginAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>marginMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("marginMode")]
    public MarginMode MarginMode { get; set; }
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>longLeverage</c>"] Long leverage
    /// </summary>
    [JsonPropertyName("longLeverage")]
    public decimal LongLeverage { get; set; }
    /// <summary>
    /// ["<c>shortLeverage</c>"] Short leverage
    /// </summary>
    [JsonPropertyName("shortLeverage")]
    public decimal ShortLeverage { get; set; }
}

