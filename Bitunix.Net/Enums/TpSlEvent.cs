using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Tp/Sl event
/// </summary>
[JsonConverter(typeof(EnumConverter<TpSlEvent>))]
public enum TpSlEvent
{
    /// <summary>
    /// ["<c>CREATE</c>"] Created
    /// </summary>
    [Map("CREATE")]
    Created,
    /// <summary>
    /// ["<c>UPDATE</c>"] Updated
    /// </summary>
    [Map("UPDATE")]
    Updated,
    /// <summary>
    /// ["<c>CLOSE</c>"] Closed
    /// </summary>
    [Map("CLOSE")]
    Closed
}
