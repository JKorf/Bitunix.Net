using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Deposit info
/// </summary>
public record BitunixDeposit
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>transactionHash</c>"] Transaction hash
    /// </summary>
    [JsonPropertyName("transactionHash")]
    public string TransactionHash { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>coin</c>"] Asset
    /// </summary>
    [JsonPropertyName("coin")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>network</c>"] Network
    /// </summary>
    [JsonPropertyName("network")]
    public string Network { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public DepositType Type { get; set; }
    /// <summary>
    /// ["<c>fromAddress</c>"] From address
    /// </summary>
    [JsonPropertyName("fromAddress")]
    public string FromAddress { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>toAddress</c>"] To address
    /// </summary>
    [JsonPropertyName("toAddress")]
    public string ToAddress { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>amount</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public WithdrawStatus Status { get; set; }
    /// <summary>
    /// ["<c>ctime</c>"] Create time
    /// </summary>
    [JsonPropertyName("ctime")]
    public DateTime CreateTime { get; set; }
}

