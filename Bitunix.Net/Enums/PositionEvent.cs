using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums
{
    /// <summary>
    /// Position event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionEvent>))]
    public enum PositionEvent
    {
        /// <summary>
        /// ["<c>OPEN</c>"] Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// ["<c>UPDATE</c>"] Update
        /// </summary>
        [Map("UPDATE")]
        Update,
        /// <summary>
        /// ["<c>CLOSE</c>"] Closed
        /// </summary>
        [Map("CLOSE")]
        Closed
    }
}
