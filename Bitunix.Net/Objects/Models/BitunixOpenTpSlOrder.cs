using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Tp/sl order
/// </summary>
public record BitunixOpenTpSlOrder
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>positionId</c>"] Position id
    /// </summary>
    [JsonPropertyName("positionId")]
    public string PositionId { get; set; } = string.Empty;
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
    /// ["<c>slOrderType</c>"] Stop loss order type
    /// </summary>
    [JsonPropertyName("slOrderType")]
    public OrderType? SlOrderType { get; set; }
    /// <summary>
    /// ["<c>slOrderPrice</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("slOrderPrice")]
    public decimal? SlOrderPrice { get; set; }
    /// <summary>
    /// ["<c>tpQty</c>"] Take profit quantity
    /// </summary>
    [JsonPropertyName("tpQty")]
    public decimal? TpQuantity { get; set; }
    /// <summary>
    /// ["<c>slQty</c>"] Stop loss quantity
    /// </summary>
    [JsonPropertyName("slQty")]
    public decimal? SlQuantity { get; set; }
}

