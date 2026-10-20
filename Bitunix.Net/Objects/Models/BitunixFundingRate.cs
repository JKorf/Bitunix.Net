using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Funding info
/// </summary>
public record BitunixFundingRate
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>markPrice</c>"] Mark price
    /// </summary>
    [JsonPropertyName("markPrice")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>lastPrice</c>"] Last price
    /// </summary>
    [JsonPropertyName("lastPrice")]
    public decimal LastPrice { get; set; }
    /// <summary>
    /// ["<c>indexPrice</c>"] Index price
    /// </summary>
    [JsonPropertyName("indexPrice")]
    public decimal IndexPrice { get; set; }
    /// <summary>
    /// ["<c>fundingRate</c>"] Funding rate
    /// </summary>
    [JsonPropertyName("fundingRate")]
    public decimal FundingRate { get; set; }
    /// <summary>
    /// ["<c>fundingInterval</c>"] Funding interval
    /// </summary>
    [JsonPropertyName("fundingInterval")]
    public int FundingInterval { get; set; }
    /// <summary>
    /// ["<c>nextFundingTime</c>"] Next funding time
    /// </summary>
    [JsonPropertyName("nextFundingTime")]
    public DateTime NextFundingTime { get; set; }
    /// <summary>
    /// ["<c>maxFundingRate</c>"] Max funding rate
    /// </summary>
    [JsonPropertyName("maxFundingRate")]
    public decimal MaxFundingRate { get; set; }
    /// <summary>
    /// ["<c>minFundingRate</c>"] Min funding rate
    /// </summary>
    [JsonPropertyName("minFundingRate")]
    public decimal MinFundingRate { get; set; }
}

