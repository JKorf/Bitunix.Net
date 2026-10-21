using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Trade side
/// </summary>
[JsonConverter(typeof(EnumConverter<TradeSide>))]
public enum TradeSide
{
    /// <summary>
    /// ["<c>OPEN</c>"] Open
    /// </summary>
    [Map("OPEN")]
    Open,
    /// <summary>
    /// ["<c>CLOSE</c>"] Close
    /// </summary>
    [Map("CLOSE")]
    Close,
}
