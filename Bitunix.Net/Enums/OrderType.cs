using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Order type
/// </summary>
[JsonConverter(typeof(EnumConverter<OrderType>))]
public enum OrderType
{
    /// <summary>
    /// ["<c>1</c>"] Limit
    /// </summary>
    [Map("1")]
    Limit,
    /// <summary>
    /// ["<c>2</c>"] Market
    /// </summary>
    [Map("2")]
    Market,
}
