using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Time in force
/// </summary>
[JsonConverter(typeof(EnumConverter<TimeInForce>))]
public enum TimeInForce
{
    /// <summary>
    /// ["<c>IOC</c>"] Immediate or cancel
    /// </summary>
    [Map("IOC")]
    ImmediateOrCancel,
    /// <summary>
    /// ["<c>FOK</c>"] Fill or kill
    /// </summary>
    [Map("FOK")]
    FillOrKill,
    /// <summary>
    /// ["<c>GTC</c>"] Good till canceled
    /// </summary>
    [Map("GTC")]
    GoodTillCanceled,
    /// <summary>
    /// ["<c>POST_ONLY</c>"] Post only
    /// </summary>
    [Map("POST_ONLY")]
    PostOnly
}
