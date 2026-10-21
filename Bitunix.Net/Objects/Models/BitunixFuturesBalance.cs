using Bitunix.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Balance
/// </summary>
public record BitunixFuturesBalance
{
    /// <summary>
    /// ["<c>marginCoin</c>"] Margin asset
    /// </summary>
    [JsonPropertyName("marginCoin")]
    public string MarginAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>available</c>"] Available
    /// </summary>
    [JsonPropertyName("available")]
    public decimal Available { get; set; }
    /// <summary>
    /// ["<c>frozen</c>"] Frozen
    /// </summary>
    [JsonPropertyName("frozen")]
    public decimal Frozen { get; set; }
    /// <summary>
    /// ["<c>margin</c>"] Position quantity
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal Margin { get; set; }
    /// <summary>
    /// ["<c>transfer</c>"] Transfer
    /// </summary>
    [JsonPropertyName("transfer")]
    public decimal Transferable { get; set; }
    /// <summary>
    /// ["<c>positionMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("positionMode")]
    public PositionMode PositionMode { get; set; }
    /// <summary>
    /// ["<c>crossUnrealizedPNL</c>"] Cross margin unrealized profit and loss
    /// </summary>
    [JsonPropertyName("crossUnrealizedPNL")]
    public decimal CrossUnrealizedPnl { get; set; }
    /// <summary>
    /// ["<c>isolationUnrealizedPNL</c>"] Isolation margin unrealized profit and loss
    /// </summary>
    [JsonPropertyName("isolationUnrealizedPNL")]
    public decimal IsolationUnrealizedPnl { get; set; }
    /// <summary>
    /// ["<c>bonus</c>"] Bonus
    /// </summary>
    [JsonPropertyName("bonus")]
    public decimal Bonus { get; set; }
}

