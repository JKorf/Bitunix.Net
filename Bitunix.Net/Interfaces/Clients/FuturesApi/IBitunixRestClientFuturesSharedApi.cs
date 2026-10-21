using CryptoExchange.Net.SharedApis;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IBitunixRestClientFuturesSharedApi :
        IGetBalancesRest,
        IGetFundingInfoRest,
        IGetFundingRateHistoryRest,
        IGetMarkPriceRest,
        IGetAllMarkPricesRest,
        IGetKlinesRest,
        IGetMarkPriceKlinesRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetLeverageTiersRest,
        IGetBookTickerRest,
        IGetOrderBookRest,
        IGetPositionModeRest,
        ISetPositionModeRest,
        IGetFuturesSymbolsRest,
        IGetTickerRest,
        IGetAllTickersRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetFuturesOrderByClientOrderIdRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        ICancelFuturesOrderRest,
        ICancelFuturesOrderByClientOrderIdRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelAllFuturesSymbolOrdersRest,
        ICancelAllFuturesOrdersRest,
        IEditFuturesOrderRest,
        IEditFuturesOrderByClientOrderIdRest,
        IGetPositionsRest,
        IGetPositionHistoryRest,
        ISetFuturesTpSlRest,
        ICancelFuturesTpSlRest
    {
    }
}
