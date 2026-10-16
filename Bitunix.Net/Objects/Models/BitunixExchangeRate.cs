using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Exchange rate
/// </summary>
public record BitunixExchangeRate
{
    /// <summary>
    /// ["<c>baseSymbol</c>"] Base asset
    /// </summary>
    [JsonPropertyName("baseSymbol")]
    public string BaseAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>quoteSymbol</c>"] Quote asset
    /// </summary>
    [JsonPropertyName("quoteSymbol")]
    public string QuoteAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>rate</c>"] Exchange rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal ExchangeRate { get; set; }
}

