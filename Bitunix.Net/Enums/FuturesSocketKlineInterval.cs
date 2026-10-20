using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Kline interval
/// </summary>
[JsonConverter(typeof(EnumConverter<FuturesSocketKlineInterval>))]
public enum FuturesSocketKlineInterval
{
    /// <summary>
    /// ["<c>1m</c>"] One minute
    /// </summary>
    [Map("1min")]
    OneMinute = 60,
    /// <summary>
    /// ["<c>3m</c>"] Three minute
    /// </summary>
    [Map("3min")]
    ThreeMinutes = 60 * 3,
    /// <summary>
    /// ["<c>5m</c>"] Five minutes
    /// </summary>
    [Map("5min")]
    FiveMinutes = 60 * 5,
    /// <summary>
    /// ["<c>15m</c>"] Fifteen minutes
    /// </summary>
    [Map("15min")]
    FifteenMinutes = 60 * 15,
    /// <summary>
    /// ["<c>30m</c>"] Thirty minutes
    /// </summary>
    [Map("30min")]
    ThirtyMinutes = 60 * 30,
    /// <summary>
    /// ["<c>1h</c>"] One hour
    /// </summary>
    [Map("60min")]
    OneHour = 60 * 60,
    /// <summary>
    /// ["<c>2h</c>"] Two hours
    /// </summary>
    [Map("2h")]
    TwoHours = 60 * 60 * 2,
    /// <summary>
    /// ["<c>4h</c>"] Four hours
    /// </summary>
    [Map("4h")]
    FourHours = 60 * 60 * 4,
    /// <summary>
    /// ["<c>6h</c>"] Six hours
    /// </summary>
    [Map("6h")]
    SixHours = 60 * 60 * 6,
    /// <summary>
    /// ["<c>8h</c>"] Eight hours
    /// </summary>
    [Map("8h")]
    EightHours = 60 * 60 * 8,
    /// <summary>
    /// ["<c>12h</c>"] Twelve hours
    /// </summary>
    [Map("12h")]
    TwelveHours = 60 * 60 * 12,
    /// <summary>
    /// ["<c>1day</c>"] One day
    /// </summary>
    [Map("1day")]
    OneDay = 60 * 60 * 24,
    /// <summary>
    /// ["<c>3day</c>"] Three days
    /// </summary>
    [Map("3day")]
    ThreeDays = 60 * 60 * 24 * 3,
    /// <summary>
    /// ["<c>1week</c>"] One week
    /// </summary>
    [Map("1week")]
    OneWeek = 60 * 60 * 24 * 7,
    /// <summary>
    /// ["<c>1month</c>"] One month
    /// </summary>
    [Map("1month")]
    OneMonth = 60 * 60 * 24 * 30,
}
