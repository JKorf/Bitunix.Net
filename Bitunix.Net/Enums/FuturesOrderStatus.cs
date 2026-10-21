using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Order status
/// </summary>
[JsonConverter(typeof(EnumConverter<FuturesOrderStatus>))]
public enum FuturesOrderStatus
{
    /// <summary>
    /// ["<c>CANCELED</c>"] Canceled
    /// </summary>
    [Map("CANCELED")]
    Canceled,
    /// <summary>
    /// ["<c>INIT</c>"] Initial
    /// </summary>
    [Map("INIT")]
    Initial,
    /// <summary>
    /// ["<c>NEW</c>"] New
    /// </summary>
    [Map("NEW", "NEW_")]
    New,
    /// <summary>
    /// ["<c>PART_FILLED</c>"] Partially filled
    /// </summary>
    [Map("PART_FILLED")]
    PartiallyFilled,
    /// <summary>
    /// ["<c>FILLED</c>"] Filled
    /// </summary>
    [Map("FILLED")]
    Filled,
}
