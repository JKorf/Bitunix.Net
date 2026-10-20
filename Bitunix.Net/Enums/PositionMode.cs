using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Position mode
/// </summary>
[JsonConverter(typeof(EnumConverter<PositionMode>))]
public enum PositionMode
{
    /// <summary>
    /// ["<c>HEDGE</c>"] Hedge mode
    /// </summary>
    [Map("HEDGE")]
    HedgeMode,
    /// <summary>
    /// ["<c>ONE_WAY</c>"] One way
    /// </summary>
    [Map("ONE_WAY")]
    OneWay,
}
