using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Kline interval
/// </summary>
[JsonConverter(typeof(EnumConverter<KlineInterval>))]
public enum KlineInterval
{
    /// <summary>
    /// ["<c>1</c>"] One minute
    /// </summary>
    [Map("1")]
    OneMinute,
    /// <summary>
    /// ["<c>3</c>"] Three minutes
    /// </summary>
    [Map("3")]
    ThreeMinutes,
    /// <summary>
    /// ["<c>5</c>"] Five minutes
    /// </summary>
    [Map("5")]
    FiveMinutes,
    /// <summary>
    /// ["<c>15</c>"] Fifteen minutes
    /// </summary>
    [Map("15")]
    FifteenMinutes,
    /// <summary>
    /// ["<c>30</c>"] Thirty minutes
    /// </summary>
    [Map("30")]
    ThirtyMinutes,
    /// <summary>
    /// ["<c>60</c>"] One hour
    /// </summary>
    [Map("60")]
    OneHour,
    /// <summary>
    /// ["<c>120</c>"] Two hours
    /// </summary>
    [Map("120")]
    TwoHours,
    /// <summary>
    /// ["<c>240</c>"] Four hours
    /// </summary>
    [Map("240")]
    FourHours,
    /// <summary>
    /// ["<c>360</c>"] Six hours
    /// </summary>
    [Map("360")]
    SixHours,
    /// <summary>
    /// ["<c>720</c>"] Twelve hours
    /// </summary>
    [Map("720")]
    TwelveHours,
    /// <summary>
    /// ["<c>D</c>"] One day
    /// </summary>
    [Map("D")]
    OneDay,
    /// <summary>
    /// ["<c>W</c>"] One week
    /// </summary>
    [Map("W")]
    OneWeek,
    /// <summary>
    /// ["<c>M</c>"] One month
    /// </summary>
    [Map("M")]
    OneMonth,
}
