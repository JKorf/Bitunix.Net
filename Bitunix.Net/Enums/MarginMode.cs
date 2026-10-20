using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Margin mode
/// </summary>
[JsonConverter(typeof(EnumConverter<MarginMode>))]
public enum MarginMode
{
    /// <summary>
    /// ["<c>1</c>"] Isolated
    /// </summary>
    [Map("ISOLATION", "1")]
    Isolated,
    /// <summary>
    /// ["<c>2</c>"] Cross
    /// </summary>
    [Map("CROSS", "2")]
    Cross,
}
