using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums
{
    /// <summary>
    /// Order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderEvent>))]
    public enum OrderEvent
    {
        /// <summary>
        /// ["<c>CREATE</c>"] Create
        /// </summary>
        [Map("CREATE")]
        Created,
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
