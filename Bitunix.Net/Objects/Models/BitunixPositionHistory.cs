using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Position history page
/// </summary>
public record BitunixPositionHistoryPage
{
    /// <summary>
    /// ["<c>positionList</c>"] Positions
    /// </summary>
    [JsonPropertyName("positionList")]
    public BitunixPositionHistory[] Positions { get; set; } = [];
    /// <summary>
    /// ["<c>total</c>"] Total
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
}

/// <summary>
/// Position history
/// </summary>
public record BitunixPositionHistory
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
    /// ["<c>maxQty</c>"] Max quantity
    /// </summary>
    [JsonPropertyName("maxQty")]
    public decimal MaxQuantity { get; set; }
    /// <summary>
    /// ["<c>entryPrice</c>"] Average entry price
    /// </summary>
    [JsonPropertyName("entryPrice")]
    public decimal AverageEntryPrice { get; set; }
    /// <summary>
    /// ["<c>closePrice</c>"] Average close price
    /// </summary>
    [JsonPropertyName("closePrice")]
    public decimal AverageClosePrice { get; set; }
    /// <summary>
    /// ["<c>liqQty</c>"] Liquidation quantity
    /// </summary>
    [JsonPropertyName("liqQty")]
    public decimal LiquidationQuantity { get; set; }
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public PositionSide Side { get; set; }
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
    /// ["<c>liqPrice</c>"] Liquidation price
    /// </summary>
    [JsonPropertyName("liqPrice")]
    public decimal LiquidationPrice { get; set; }
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

