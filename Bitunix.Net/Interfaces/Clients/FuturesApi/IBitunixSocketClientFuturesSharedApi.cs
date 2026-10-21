using CryptoExchange.Net.SharedApis;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IBitunixSocketClientFuturesSharedApi :
        ISubscribeTradesSocket,
        ISubscribeTickerSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeFundingInfoSocket,
        ISubscribeMarkPriceSocket,
        ISubscribeIndexPriceSocket,
        ISubscribeBalancesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket
    {
    }
}
