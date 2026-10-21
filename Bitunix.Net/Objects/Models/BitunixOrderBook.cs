using System;
using System.Text.Json.Serialization;
using CryptoExchange.Net.Interfaces;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Order book
/// </summary>
public record BitunixOrderBook
{
    /// <summary>
    /// ["<c>asks</c>"] Asks
    /// </summary>
    [JsonPropertyName("asks")]
    public BitunixOrderBookEntry[] Asks { get; set; } = [];
    /// <summary>
    /// ["<c>bids</c>"] Bids
    /// </summary>
    [JsonPropertyName("bids")]
    public BitunixOrderBookEntry[] Bids { get; set; } = [];
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Order book entry
/// </summary>
public record BitunixOrderBookEntry : ISymbolOrderBookEntry
{
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>volume</c>"] Quantity
    /// </summary>
    [JsonPropertyName("volume")]
    public decimal Quantity { get; set; }
}


