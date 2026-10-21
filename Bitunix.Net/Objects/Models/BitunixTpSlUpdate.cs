using Bitunix.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Tp/Sl update info
/// </summary>
public record BitunixTpSlUpdate
{
    /// <summary>
    /// ["<c>event</c>"] Tp/Sl event
    /// </summary>
    [JsonPropertyName("event")]
    public TpSlEvent Event { get; set; }
    /// <summary>
    /// ["<c>positionId</c>"] Position id
    /// </summary>
    [JsonPropertyName("positionId")]
    public string PositionId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide OrderSide { get; set; }
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>positionMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("positionMode")]
    public PositionMode PositionMode { get; set; }
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public TpSlStatus Status { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType OrderType { get; set; }

    /// <summary>
    /// ["<c>tpQty</c>"] Take profit quantity
    /// </summary>
    [JsonPropertyName("tpQty")]
    public decimal TpQuantity { get; set; }
    /// <summary>
    /// ["<c>tpStopType</c>"] Take profit price type
    /// </summary>
    [JsonPropertyName("tpStopType")]
    public PriceType TpPriceType { get; set; }
    /// <summary>
    /// ["<c>tpPrice</c>"] Take profit price
    /// </summary>
    [JsonPropertyName("tpPrice")]
    public decimal TpPrice { get; set; }
    /// <summary>
    /// ["<c>tpOrderType</c>"] Take profit order type
    /// </summary>
    [JsonPropertyName("tpOrderType")]
    public OrderType TpOrderType { get; set; }
    /// <summary>
    /// ["<c>tpOrderPrice</c>"] Take profit order price
    /// </summary>
    [JsonPropertyName("tpOrderPrice")]
    public decimal TpOrderPrice { get; set; }

    /// <summary>
    /// ["<c>slQty</c>"] Stop loss quantity
    /// </summary>
    [JsonPropertyName("slQty")]
    public decimal SlQuantity { get; set; }
    /// <summary>
    /// ["<c>slStopType</c>"] Stop loss price type
    /// </summary>
    [JsonPropertyName("slStopType")]
    public PriceType SlPriceType { get; set; }
    /// <summary>
    /// ["<c>slPrice</c>"] Stop loss price
    /// </summary>
    [JsonPropertyName("slPrice")]
    public decimal SlPrice { get; set; }
    /// <summary>
    /// ["<c>slOrderType</c>"] Stop loss order type
    /// </summary>
    [JsonPropertyName("slOrderType")]
    public OrderType SlOrderType { get; set; }
    /// <summary>
    /// ["<c>slOrderPrice</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrderPrice")]
    public decimal SlOrderPrice { get; set; }


}

