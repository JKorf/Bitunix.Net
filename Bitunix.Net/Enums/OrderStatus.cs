using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Order status
/// </summary>
[JsonConverter(typeof(EnumConverter<OrderStatus>))]
public enum OrderStatus
{
    /// <summary>
    /// ["<c>1</c>"] Open
    /// </summary>
    [Map("1")]
    Open,
    /// <summary>
    /// ["<c>2</c>"] Filled
    /// </summary>
    [Map("2")]
    Filled,
    /// <summary>
    /// ["<c>3</c>"] Partially Filled
    /// </summary>
    [Map("3")]
    PartiallyFilled,
    /// <summary>
    /// ["<c>4</c>"] Canceled
    /// </summary>
    [Map("4")]
    Canceled,
    /// <summary>
    /// ["<c>7</c>"] Partially Canceled
    /// </summary>
    [Map("7")]
    PartiallyCanceled,
}
