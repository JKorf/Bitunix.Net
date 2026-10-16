using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Withdrawal info
/// </summary>
public record BitunixWithdrawal
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>outBizId</c>"] Out biz id
    /// </summary>
    [JsonPropertyName("outBizId")]
    public string? OutBizId { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public WithdrawType Type { get; set; }
    /// <summary>
    /// ["<c>coin</c>"] Asset
    /// </summary>
    [JsonPropertyName("coin")]
    public string Asset { get; set; } = string.Empty;
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
    /// <summary>
    /// ["<c>network</c>"] Network
    /// </summary>
    [JsonPropertyName("network")]
    public string Network { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>address</c>"] Address
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    /// <summary>
    /// ["<c>transactionHash</c>"] Transaction hash
    /// </summary>
    [JsonPropertyName("transactionHash")]
    public string? TransactionHash { get; set; }
    /// <summary>
    /// ["<c>feeCoin</c>"] Fee asset
    /// </summary>
    [JsonPropertyName("feeCoin")]
    public string? FeeAsset { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public string? Fee { get; set; }
    /// <summary>
    /// ["<c>memo</c>"] Memo
    /// </summary>
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }
    /// <summary>
    /// ["<c>toType</c>"] To type
    /// </summary>
    [JsonPropertyName("toType")]
    public string? ToType { get; set; }
    /// <summary>
    /// ["<c>toUid</c>"] To uid
    /// </summary>
    [JsonPropertyName("toUid")]
    public string? ToUid { get; set; }
    /// <summary>
    /// ["<c>toPhoneCountryCode</c>"] To phone country code
    /// </summary>
    [JsonPropertyName("toPhoneCountryCode")]
    public string? ToPhoneCountryCode { get; set; }
    /// <summary>
    /// ["<c>toPhoneNumber</c>"] To phone number
    /// </summary>
    [JsonPropertyName("toPhoneNumber")]
    public string? ToPhoneNumber { get; set; }
    /// <summary>
    /// ["<c>toEmail</c>"] To email
    /// </summary>
    [JsonPropertyName("toEmail")]
    public string? ToEmail { get; set; }
    /// <summary>
    /// ["<c>remark</c>"] Remark
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
}

