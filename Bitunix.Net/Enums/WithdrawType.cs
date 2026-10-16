using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Withdraw type
/// </summary>
[JsonConverter(typeof(EnumConverter<WithdrawType>))]
public enum WithdrawType
{
    /// <summary>
    /// ["<c>withdraw</c>"] Withdrawal
    /// </summary>
    [Map("withdraw")]
    Withdrawal,
    /// <summary>
    /// ["<c>inside_transfer_out</c>"] Internal transfer
    /// </summary>
    [Map("inside_transfer_out")]
    InternalTransfer,
}
