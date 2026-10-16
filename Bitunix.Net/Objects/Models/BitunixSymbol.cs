using Bitunix.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// 
/// </summary>
public record BitunixSymbol
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol name
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>base</c>"] Base asset
    /// </summary>
    [JsonPropertyName("base")]
    public string BaseAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>baseIcon</c>"] Base asset icon
    /// </summary>
    [JsonPropertyName("baseIcon")]
    public string BaseAssetIcon { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>quote</c>"] Quote asset
    /// </summary>
    [JsonPropertyName("quote")]
    public string QuoteAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>quoteIcon</c>"] Quote asset icon
    /// </summary>
    [JsonPropertyName("quoteIcon")]
    public string QuoteAssetIcon { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>basePrecision</c>"] Base asset precision
    /// </summary>
    [JsonPropertyName("basePrecision")]
    public int BaseAssetPrecision { get; set; }
    /// <summary>
    /// ["<c>quotePrecision</c>"] Quote asset precision
    /// </summary>
    [JsonPropertyName("quotePrecision")]
    public int QuoteAssetPrecision { get; set; }
    /// <summary>
    /// ["<c>minPrice</c>"] Min order value
    /// </summary>
    [JsonPropertyName("minPrice")]
    public decimal MinOrderValue { get; set; }
    /// <summary>
    /// ["<c>minVolume</c>"] Min order quantity
    /// </summary>
    [JsonPropertyName("minVolume")]
    public decimal MinOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>maxAmount</c>"] Max order quantity
    /// </summary>
    [JsonPropertyName("maxAmount")]
    public decimal MaxOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>minAmount</c>"] Min amount
    /// </summary>
    [JsonPropertyName("minAmount")]
    public decimal MinAmount { get; set; }
    /// <summary>
    /// ["<c>maxLimitOrderAmount</c>"] Max limit order quantity
    /// </summary>
    [JsonPropertyName("maxLimitOrderAmount")]
    public decimal? MaxLimitOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>maxMarketOrderAmount</c>"] Max market order quantity
    /// </summary>
    [JsonPropertyName("maxMarketOrderAmount")]
    public decimal? MaxMarketOrderQuantity { get; set; }
    /// <summary>
    /// ["<c>maxSlippage</c>"] Max slippage
    /// </summary>
    [JsonPropertyName("maxSlippage")]
    public decimal MaxSlippage { get; set; }
    /// <summary>
    /// ["<c>premiumFactor</c>"] Premium factor
    /// </summary>
    [JsonPropertyName("premiumFactor")]
    public decimal PremiumFactor { get; set; }
    /// <summary>
    /// ["<c>minBuyPriceOffset</c>"] Min buy price offset
    /// </summary>
    [JsonPropertyName("minBuyPriceOffset")]
    public decimal MinBuyPriceOffset { get; set; }
    /// <summary>
    /// ["<c>maxSellPriceOffset</c>"] Max sell price offset
    /// </summary>
    [JsonPropertyName("maxSellPriceOffset")]
    public decimal MaxSellPriceOffset { get; set; }
    /// <summary>
    /// ["<c>isOpen</c>"] Is open
    /// </summary>
    [JsonPropertyName("isOpen")]
    public bool IsOpen { get; set; }
    /// <summary>
    /// ["<c>isHot</c>"] Is hot
    /// </summary>
    [JsonPropertyName("isHot")]
    public bool IsHot { get; set; }
    /// <summary>
    /// ["<c>isRecommend</c>"] Is recommend
    /// </summary>
    [JsonPropertyName("isRecommend")]
    public bool IsRecommend { get; set; }
    /// <summary>
    /// ["<c>isShow</c>"] Is show
    /// </summary>
    [JsonPropertyName("isShow")]
    public bool IsShow { get; set; }
    /// <summary>
    /// ["<c>tradeArea</c>"] Trade area
    /// </summary>
    [JsonPropertyName("tradeArea")]
    public string TradeArea { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>sort</c>"] Sort
    /// </summary>
    [JsonPropertyName("sort")]
    public int Sort { get; set; }
    /// <summary>
    /// ["<c>openTime</c>"] Open time
    /// </summary>
    [JsonPropertyName("openTime")]
    public DateTime? OpenTime { get; set; }
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>precisions</c>"] Precisions
    /// </summary>
    [JsonPropertyName("precisions")]
    public decimal[] Precisions { get; set; } = [];
    /// <summary>
    /// ["<c>fullName</c>"] Full name
    /// </summary>
    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;
}
