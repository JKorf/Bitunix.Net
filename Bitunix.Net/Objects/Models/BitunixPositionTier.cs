using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Position tier
/// </summary>
public record BitunixPositionTier
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public int Level { get; set; }
    /// <summary>
    /// ["<c>startValue</c>"] Start value
    /// </summary>
    [JsonPropertyName("startValue")]
    public decimal StartValue { get; set; }
    /// <summary>
    /// ["<c>endValue</c>"] End value
    /// </summary>
    [JsonPropertyName("endValue")]
    public decimal? EndValue { get; set; }
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>maintenanceMarginRate</c>"] Maintenance margin rate
    /// </summary>
    [JsonPropertyName("maintenanceMarginRate")]
    public decimal MaintenanceMarginRate { get; set; }
}

