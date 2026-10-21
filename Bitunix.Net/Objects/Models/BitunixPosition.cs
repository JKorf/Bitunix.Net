using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Position info
/// </summary>
public record BitunixPosition
{
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
    /// ["<c>qty</c>"] Quantity
    /// </summary>
    [JsonPropertyName("qty")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>entryValue</c>"] Entry value
    /// </summary>
    [JsonPropertyName("entryValue")]
    public decimal EntryValue { get; set; }
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
    /// ["<c>funding</c>"] Total funding fee
    /// </summary>
    [JsonPropertyName("funding")]
    public decimal Funding { get; set; }
    /// <summary>
    /// ["<c>realizedPNL</c>"] Realized profit and loss
    /// </summary>
    [JsonPropertyName("realizedPNL")]
    public decimal RealizedPnl { get; set; }
    /// <summary>
    /// ["<c>margin</c>"] Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal Margin { get; set; }
    /// <summary>
    /// ["<c>unrealizedPNL</c>"] Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("unrealizedPNL")]
    public decimal UnrealizedPnl { get; set; }
    /// <summary>
    /// ["<c>liqPrice</c>"] Liquidation price
    /// </summary>
    [JsonPropertyName("liqPrice")]
    public decimal LiquidationPrice { get; set; }
    /// <summary>
    /// ["<c>marginRate</c>"] Margin rate
    /// </summary>
    [JsonPropertyName("marginRate")]
    public decimal MarginRate { get; set; }
    /// <summary>
    /// ["<c>avgOpenPrice</c>"] Average open price
    /// </summary>
    [JsonPropertyName("avgOpenPrice")]
    public decimal AverageOpenPrice { get; set; }
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
}

