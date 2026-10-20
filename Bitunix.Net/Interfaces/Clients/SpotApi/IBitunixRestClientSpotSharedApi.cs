using CryptoExchange.Net.SharedApis;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IBitunixRestClientSpotSharedApi :
        IGetAllAssetsRest,
        IGetBalancesRest,
        IGetDepositHistoryRest,
        IGetDepositAddressesRest,
        IGetKlinesRest,
        IGetOrderBookRest,
        IGetSpotSymbolsRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,

        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        ICancelSpotOrderRest
    {
    }
}
