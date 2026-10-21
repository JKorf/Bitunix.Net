using Bitunix.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Order result
/// </summary>
public record BitunixOrderResult
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Order side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide? Side { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Order type
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType? Type { get; set; }
    /// <summary>
    /// ["<c>volume</c>"] Quantity
    /// </summary>
    [JsonPropertyName("volume")]
    public decimal? Quantity { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
    /// <summary>
    /// ["<c>placeStatus</c>"] Indicates if the order was successful
    /// </summary>
    [JsonPropertyName("placeStatus")]
    public bool Success { get; set; }
    /// <summary>
    /// ["<c>placeCode</c>"] Place code
    /// </summary>
    [JsonPropertyName("placeCode")]
    public int PlaceCode { get; set; }
    /// <summary>
    /// ["<c>placeMsg</c>"] Place message
    /// </summary>
    [JsonPropertyName("placeMsg")]
    public string PlaceMessage { get; set; } = string.Empty;
}

