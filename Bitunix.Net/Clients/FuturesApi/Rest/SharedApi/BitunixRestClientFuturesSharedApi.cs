using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Interfaces.Clients.FuturesApi;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesSharedApi :
        SharedApiBase,
        IBitunixRestClientFuturesSharedApi
    {
        private readonly BitunixRestClientFuturesApi _api;

        private const string _topicId = "BitunixFutures";
        private const string _exchangeName = "Bitunix";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);

        public BitunixRestClientFuturesSharedApi(BitunixRestClientFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api,
                  new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetFundingInfoOptions,
                GetFundingRateHistoryOptions,
                GetMarkPriceOptions,
                GetAllMarkPricesOptions,
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetLeverageTiersOptions,
                GetBookTickerOptions,
                GetOrderBookOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetFuturesSymbolsOptions,
                GetAllTickersOptions,
                GetTickerOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetFuturesOrderByClientOrderIdOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesUserTradeHistoryOptions,
                GetFuturesOrderTradesOptions,
                CancelFuturesOrderOptions,
                CancelAllFuturesOrdersOptions,
                CancelAllFuturesSymbolOrdersOptions,
                EditFuturesOrderOptions,
                EditFuturesOrderByClientOrderIdOptions
                );

        }

        private string GetMarginAsset(SharedRequest request)
        {
            var marginAsset = request.GetParamValue<string>(_exchangeName, "MarginAsset", "marginCoin");
            return marginAsset ?? "USDT";
        }
    }
}
