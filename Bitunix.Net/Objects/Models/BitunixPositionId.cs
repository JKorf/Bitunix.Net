using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Position id
/// </summary>
public record BitunixPositionId
{
    /// <summary>
    /// ["<c>positionId</c>"] Position id
    /// </summary>
    [JsonPropertyName("positionId")]
    public string PositionId { get; set; } = string.Empty;
}

