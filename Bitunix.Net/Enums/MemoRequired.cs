using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Enums;

/// <summary>
/// Memo required
/// </summary>
[JsonConverter(typeof(EnumConverter<MemoRequired>))]
public enum MemoRequired
{
    /// <summary>
    /// ["<c>0</c>"] Not needed
    /// </summary>
    [Map("0")]
    No,
    /// <summary>
    /// ["<c>1</c>"] Optional
    /// </summary>
    [Map("1")]
    Optional,
    /// <summary>
    /// ["<c>2</c>"] Required
    /// </summary>
    [Map("2")]
    Required
}
