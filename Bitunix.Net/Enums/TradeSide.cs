using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
