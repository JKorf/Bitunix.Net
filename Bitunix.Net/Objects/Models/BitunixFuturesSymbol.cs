using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Symbol info
/// </summary>
public record BitunixFuturesSymbol
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>base</c>"] Base asset
    /// </summary>
    [JsonPropertyName("base")]
    public string BaseAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>quote</c>"] Quote asset
    /// </summary>
    [JsonPropertyName("quote")]
    public string QuoteAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>minTradeVolume</c>"] Min trade quantity
    /// </summary>
    [JsonPropertyName("minTradeVolume")]
    public decimal MinTradeQuantity { get; set; }
    /// <summary>
    /// ["<c>minBuyPriceOffset</c>"] Min buy price offset
    /// </summary>
    [JsonPropertyName("minBuyPriceOffset")]
    public decimal? MinBuyPriceOffset { get; set; }
    /// <summary>
    /// ["<c>maxSellPriceOffset</c>"] Max sell price offset
    /// </summary>
    [JsonPropertyName("maxSellPriceOffset")]
    public decimal? MaxSellPriceOffset { get; set; }
    /// <summary>
    /// ["<c>maxLimitOrderVolume</c>"] Max limit order quantity
    /// </summary>
    [JsonPropertyName("maxLimitOrderVolume")]
    public decimal MaxLimitOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>maxMarketOrderVolume</c>"] Max market order quantity
    /// </summary>
    [JsonPropertyName("maxMarketOrderVolume")]
    public decimal MaxMarketOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>basePrecision</c>"] Base precision
    /// </summary>
    [JsonPropertyName("basePrecision")]
    public int BasePrecision { get; set; }
    /// <summary>
    /// ["<c>quotePrecision</c>"] Quote precision
    /// </summary>
    [JsonPropertyName("quotePrecision")]
    public int QuotePrecision { get; set; }
    /// <summary>
    /// ["<c>minLeverage</c>"] Min leverage
    /// </summary>
    [JsonPropertyName("minLeverage")]
    public decimal MinLeverage { get; set; }
    /// <summary>
    /// ["<c>maxLeverage</c>"] Max leverage
    /// </summary>
    [JsonPropertyName("maxLeverage")]
    public decimal MaxLeverage { get; set; }
    /// <summary>
    /// ["<c>defaultLeverage</c>"] Default leverage
    /// </summary>
    [JsonPropertyName("defaultLeverage")]
    public decimal DefaultLeverage { get; set; }
    /// <summary>
    /// ["<c>defaultMarginMode</c>"] Default margin mode
    /// </summary>
    [JsonPropertyName("defaultMarginMode")]
    public MarginMode DefaultMarginMode { get; set; }
    /// <summary>
    /// ["<c>priceProtectScope</c>"] Price protect scope
    /// </summary>
    [JsonPropertyName("priceProtectScope")]
    public decimal PriceProtectScope { get; set; }
    /// <summary>
    /// ["<c>symbolStatus</c>"] Symbol status
    /// </summary>
    [JsonPropertyName("symbolStatus")]
    public FuturesSymbolStatus SymbolStatus { get; set; }
    /// <summary>
    /// ["<c>isApiSupported</c>"] Is api supported
    /// </summary>
    [JsonPropertyName("isApiSupported")]
    public bool IsApiSupported { get; set; }
    /// <summary>
    /// ["<c>maxFundingRate</c>"] Max funding rate
    /// </summary>
    [JsonPropertyName("maxFundingRate")]
    public decimal MaxFundingRate { get; set; }
    /// <summary>
    /// ["<c>minFundingRate</c>"] Min funding rate
    /// </summary>
    [JsonPropertyName("minFundingRate")]
    public decimal MinFundingRate { get; set; }
}

