using Bitunix.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Order info
/// </summary>
public record BitunixOrder
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>clientId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientOrderId { get; set; }
    /// <summary>
    /// ["<c>userId</c>"] User id
    /// </summary>
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;
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
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Order type
    /// </summary>
    [JsonPropertyName("orderType")]
    public OrderType Type { get; set; }
    /// <summary>
    /// ["<c>status</c>"] Order status
    /// </summary>
    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// ["<c>volume</c>"] Quantity
    /// </summary>
    [JsonPropertyName("volume")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>amount</c>"] Quote quantity
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal QuoteQuantity { get; set; }

    /// <summary>
    /// ["<c>dealVolume</c>"] Quantity filled
    /// </summary>
    [JsonPropertyName("dealVolume")]
    public decimal FilledQuantity { get; set; }
    /// <summary>
    /// ["<c>leftVolume</c>"] Open quantity
    /// </summary>
    [JsonPropertyName("leftVolume")]
    public decimal RemainingQuantity { get; set; }
    /// <summary>
    /// ["<c>dealAmount</c>"] Quantity filled in base asset
    /// </summary>
    [JsonPropertyName("dealAmount")]
    public decimal FilledQuoteQuantity { get; set; }
    /// <summary>
    /// ["<c>leftAmount</c>"] Quantity open in base asset
    /// </summary>
    [JsonPropertyName("leftAmount")]
    public decimal RemainingQuoteQuantity { get; set; }
    /// <summary>
    /// ["<c>avgPrice</c>"] Average price
    /// </summary>
    [JsonPropertyName("avgPrice")]
    public decimal AveragePrice { get; set; }
    /// <summary>
    /// ["<c>progress</c>"] Progress
    /// </summary>
    [JsonPropertyName("progress")]
    public decimal Progress { get; set; }
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>utime</c>"] Update time
    /// </summary>
    [JsonPropertyName("utime")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>feeCoin</c>"] Fee asset
    /// </summary>
    [JsonPropertyName("feeCoin")]
    public string FeeAsset { get; set; } = string.Empty;
}

