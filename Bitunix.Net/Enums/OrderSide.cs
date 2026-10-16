using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Order side
/// </summary>
[JsonConverter(typeof(EnumConverter<OrderSide>))]
public enum OrderSide
{
    /// <summary>
    /// ["<c>1</c>"] Sell
    /// </summary>
    [Map("1")]
    Sell,
    /// <summary>
    /// ["<c>2</c>"] Buy
    /// </summary>
    [Map("2")]
    Buy,
}
