using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Sockets;
using Bitunix.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Objects;
using System;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Converters
{
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesTicker[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesOrderBook>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFundingRate>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFundingRate[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFundingEntry[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesKline[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesSymbol[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesBalance>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixTradingSetting[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixPositionMode>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixLeverageMargin>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixLeverage>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixId>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixCancelResult>))]
    [JsonSerializable(typeof(BitunixFuturesResponse))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixPositionId>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesOrderPage>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesOrder>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixFuturesUserTradePage>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixPositionTier[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixPosition[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixPositionHistoryPage>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixOpenTpSlOrder[]>))]
    [JsonSerializable(typeof(BitunixFuturesResponse<BitunixMarginMode>))]

    [JsonSerializable(typeof(BitunixPingRequest))]
    [JsonSerializable(typeof(BitunixPingResponse))]
    [JsonSerializable(typeof(BitunixFuturesSocketParamRequest))]
    [JsonSerializable(typeof(BitunixSocketConnectMessage))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixTickerUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixOrderBookUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixKlineUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixMarkPriceUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixTradeUpdate[]>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixAggTickerUpdate[]>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixBalanceUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixFuturesOrderUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixPositionUpdate>))]
    [JsonSerializable(typeof(BitunixFuturesSocketUpdate<BitunixTpSlUpdate>))]

    [JsonSerializable(typeof(BitunixFuturesCancelRequest[]))]

    [JsonSerializable(typeof(BitunixResponse<decimal>))]
    [JsonSerializable(typeof(BitunixResponse<decimal?>))]
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
