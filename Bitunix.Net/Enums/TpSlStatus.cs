using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Tp/Sl status
/// </summary>
[JsonConverter(typeof(EnumConverter<TpSlStatus>))]
public enum TpSlStatus
{
    /// <summary>
    /// ["<c>NEW</c>"] New
    /// </summary>
    [Map("NEW")]
    New,
    /// <summary>
    /// ["<c>CANCELED</c>"] Canceled
    /// </summary>
    [Map("CANCELED")]
    Canceled,
    /// <summary>
    /// ["<c>SYSTEM_CANCELED</c>"] System Canceled
    /// </summary>
    [Map("SYSTEM_CANCELED")]
    SystemCanceled,
    /// <summary>
    /// ["<c>FILLED</c>"] Filled
    /// </summary>
    [Map("FILLED")]
    Filled,
    /// <summary>
    /// ["<c>FAILED</c>"] Failed
    /// </summary>
    [Map("FAILED")]
    Failed,
}
