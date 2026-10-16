using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Deposit type
/// </summary>
[JsonConverter(typeof(EnumConverter<DepositType>))]
public enum DepositType
{
    /// <summary>
    /// ["<c>deposit</c>"] Deposit
    /// </summary>
    [Map("deposit")]
    Deposit,
    /// <summary>
    /// ["<c>inside_transfer_in</c>"] Internal transfer
    /// </summary>
    [Map("inside_transfer_in")]
    InternalTransfer,
}
