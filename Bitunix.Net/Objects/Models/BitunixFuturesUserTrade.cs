using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// User trade page
/// </summary>
public record BitunixFuturesUserTradePage
{
    /// <summary>
    /// ["<c>tradeList</c>"] Trades
    /// </summary>
    [JsonPropertyName("tradeList")]
    public BitunixFuturesUserTrade[] Trades { get; set; } = [];
    /// <summary>
    /// ["<c>total</c>"] Total
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
}

/// <summary>
/// User trade info
/// </summary>
public record BitunixFuturesUserTrade
{
    /// <summary>
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>qty</c>"] Quantity
    /// </summary>
    [JsonPropertyName("qty")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>positionMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("positionMode")]
    public PositionMode PositionMode { get; set; }
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
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>realizedPNL</c>"] Realized p n l
    /// </summary>
    [JsonPropertyName("realizedPNL")]
    public decimal? RealizedPNL { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType Type { get; set; }
    /// <summary>
    /// ["<c>effect</c>"] Time in force
    /// </summary>
    [JsonPropertyName("effect")]
    public TimeInForce? TimeInForce { get; set; }
    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }
    /// <summary>
    /// ["<c>clientId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientOrderId { get; set; }
    /// <summary>
    /// ["<c>source</c>"] Source
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>roleType</c>"] Trade role
    /// </summary>
    [JsonPropertyName("roleType")]
    public TradeRole TradeRole { get; set; }
}

