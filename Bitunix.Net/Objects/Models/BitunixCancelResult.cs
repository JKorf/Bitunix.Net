using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Cancel result
/// </summary>
public record BitunixCancelResult
{
    /// <summary>
    /// ["<c>successList</c>"] Success orders
    /// </summary>
    [JsonPropertyName("successList")]
    public BitunixCancelResultOrder[] Success { get; set; } = [];
    /// <summary>
    /// ["<c>failureList</c>"] Failed orders
    /// </summary>
    [JsonPropertyName("failureList")]
    public BitunixCancelResultOrder[] Failed { get; set; } = [];
}

/// <summary>
/// Order result
/// </summary>
public record BitunixCancelResultOrder
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>clientId</c>"] Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
    /// <summary>
    /// ["<c>errorMsg</c>"] Error msg
    /// </summary>
    [JsonPropertyName("errorMsg")]
    public string? ErrorMsg { get; set; }
    /// <summary>
    /// ["<c>errorCode</c>"] Error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public int? ErrorCode { get; set; }
}

