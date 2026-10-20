using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Symbol status
/// </summary>
[JsonConverter(typeof(EnumConverter<FuturesSymbolStatus>))]
public enum FuturesSymbolStatus
{
    /// <summary>
    /// ["<c>OPEN</c>"] Open
    /// </summary>
    [Map("OPEN")]
    Open,
    /// <summary>
    /// ["<c>CANCEL_ONLY</c>"] Only canceling orders allowed
    /// </summary>
    [Map("CANCEL_ONLY")]
    CancelOnly,
    /// <summary>
    /// ["<c>STOP</c>"] Stopped
    /// </summary>
    [Map("STOP")]
    Stopped,
}
