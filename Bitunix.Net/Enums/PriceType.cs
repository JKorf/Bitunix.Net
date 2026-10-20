using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Price type
/// </summary>
[JsonConverter(typeof(EnumConverter<PriceType>))]
public enum PriceType
{
    /// <summary>
    /// ["<c>LAST_PRICE</c>"] Last trade price
    /// </summary>
    [Map("LAST_PRICE")]
    LastPrice,
    /// <summary>
    /// ["<c>MARK_PRICE</c>"] Mark price
    /// </summary>
    [Map("MARK_PRICE", "MARK")]
    MarkPrice,
}
