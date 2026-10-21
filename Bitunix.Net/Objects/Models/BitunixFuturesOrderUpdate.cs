using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Order info
/// </summary>
public record BitunixFuturesOrderUpdate
{
    /// <summary>
    /// ["<c>event</c>"] Order event
    /// </summary>
    [JsonPropertyName("event")]
    public OrderEvent Event { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>positionMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("positionMode")]
    public PositionMode PositionMode { get; set; }
    /// <summary>
    /// ["<c>positionType</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("positionType")]
    public MarginMode MarginMode { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType Type { get; set; }
    /// <summary>
    /// ["<c>effect</c>"] Time in force
    /// </summary>
    [JsonPropertyName("effect")]
    public TimeInForce TimeInForce { get; set; }
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>qty</c>"] Quantity
    /// </summary>
    [JsonPropertyName("qty")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>mtime</c>"] Update time
    /// </summary>
    [JsonPropertyName("mtime")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>orderStatus</c>"] Status
    /// </summary>
    [JsonPropertyName("orderStatus")]
    public FuturesOrderStatus Status { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>averagePrice</c>"] Average price
    /// </summary>
    [JsonPropertyName("averagePrice")]
    public decimal? AveragePrice { get; set; }
    /// <summary>
    /// ["<c>clientId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientOrderId { get; set; }
    /// <summary>
    /// ["<c>dealAmount</c>"] Quantity filled
    /// </summary>
    [JsonPropertyName("dealAmount")]
    public decimal QuantityFilled { get; set; }
    /// <summary>
    /// ["<c>tpPrice</c>"] Take profit price
    /// </summary>
    [JsonPropertyName("tpPrice")]
    public decimal? TpPrice { get; set; }
    /// <summary>
    /// ["<c>tpStopType</c>"] Take profit price type
    /// </summary>
    [JsonPropertyName("tpStopType")]
    public PriceType? TpPriceType { get; set; }
    /// <summary>
    /// ["<c>tpOrderType</c>"] Take profit order type
    /// </summary>
    [JsonPropertyName("tpOrderType")]
    public OrderType? TpOrderType { get; set; }
    /// <summary>
    /// ["<c>tpOrderPrice</c>"] Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrderPrice")]
    public decimal? TpOrderPrice { get; set; }
    /// <summary>
    /// ["<c>slPrice</c>"] Stop loss price
    /// </summary>
    [JsonPropertyName("slPrice")]
    public decimal? SlPrice { get; set; }
    /// <summary>
    /// ["<c>slStopType</c>"] Stop loss price type
    /// </summary>
    [JsonPropertyName("slStopType")]
    public PriceType? SlPriceType { get; set; }
    /// <summary>
    /// ["<c>slOrderType</c>"] Stop loss order type
    /// </summary>
    [JsonPropertyName("slOrderType")]
    public OrderType? SlOrderType { get; set; }
    /// <summary>
    /// ["<c>slOrderPrice</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrderPrice")]
    public decimal? SlOrderPrice { get; set; }
}

