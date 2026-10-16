using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Withdrawal status
/// </summary>
[JsonConverter(typeof(EnumConverter<WithdrawStatus>))]
public enum WithdrawStatus
{
    /// <summary>
    /// ["<c>pending</c>"] Pending
    /// </summary>
    [Map("pending")]
    Pending,
    /// <summary>
    /// ["<c>verification</c>"] Pending user security authentication
    /// </summary>
    [Map("verification")]
    PendingVerification,
    /// <summary>
    /// ["<c>cancelled</c>"] Cancelled
    /// </summary>
    [Map("cancelled")]
    Canceled,
    /// <summary>
    /// ["<c>success</c>"] Completed
    /// </summary>
    [Map("success")]
    Success,
    /// <summary>
    /// ["<c>fail</c>"] Failed
    /// </summary>
    [Map("fail")]
    Failed,
}
