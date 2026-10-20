using System;
using System.Text.Json.Serialization;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Mark price update
/// </summary>
public record BitunixMarkPriceUpdate
{
    /// <summary>
    /// ["<c>mp</c>"] Mark price
    /// </summary>
    [JsonPropertyName("mp")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>ip</c>"] Index price
    /// </summary>
    [JsonPropertyName("ip")]
    public decimal IndexPrice { get; set; }
    /// <summary>
    /// ["<c>fr</c>"] Funding rate
    /// </summary>
    [JsonPropertyName("fr")]
    public decimal FundingRate { get; set; }
    /// <summary>
    /// ["<c>ft</c>"] Funding time
    /// </summary>
    [JsonPropertyName("ft")]
    public DateTime FundingTime { get; set; }
    /// <summary>
    /// ["<c>nft</c>"] Next funding time
    /// </summary>
    [JsonPropertyName("nft")]
    public DateTime NextFundingTime { get; set; }
}

