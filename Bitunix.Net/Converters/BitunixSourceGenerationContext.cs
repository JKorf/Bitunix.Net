using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Converters
{
    [JsonSerializable(typeof(BitunixResponse<decimal>))]
    [JsonSerializable(typeof(BitunixResponse<string>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixOrderBook>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixKline[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixSymbol[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixExchangeRate[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixAsset[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixOrderResult>))]
    [JsonSerializable(typeof(BitunixCancelOrderRequest[]))]
    [JsonSerializable(typeof(BitunixResponse<BitunixPage<BitunixOrder>>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixOrder[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixOrder>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixUserTrade[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixBalance[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixWithdrawal[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixDeposit[]>))]
    [JsonSerializable(typeof(BitunixResponse<BitunixDepositAddress[]>))]
    [JsonSerializable(typeof(BitunixSocketRequest))]
    [JsonSerializable(typeof(BitunixSocketResponse<BitunixOrderResult>))]

    [JsonSerializable(typeof(Parameters))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(decimal?))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSerializable(typeof(DateTime?))]
    internal partial class BitunixSourceGenerationContext : JsonSerializerContext
    {
    }
}
