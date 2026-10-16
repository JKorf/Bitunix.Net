using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Transfer type
/// </summary>
[JsonConverter(typeof(EnumConverter<TransferType>))]
public enum TransferType
{
    /// <summary>
    /// ["<c>spot_futures</c>"] Spot to futures account
    /// </summary>
    [Map("spot_futures")]
    SpotToFutures,
    /// <summary>
    /// ["<c>futures_spot</c>"] Futures to spot account
    /// </summary>
    [Map("futures_spot")]
    FuturesToSpot,
}
