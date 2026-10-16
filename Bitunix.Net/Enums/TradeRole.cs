using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Trade role
/// </summary>
[JsonConverter(typeof(EnumConverter<TradeRole>))]
public enum TradeRole
{
    /// <summary>
    /// ["<c>1</c>"] Maker
    /// </summary>
    [Map("1")]
    Maker,
    /// <summary>
    /// ["<c>2</c>"] Taker
    /// </summary>
    [Map("2")]
    Taker,
}
