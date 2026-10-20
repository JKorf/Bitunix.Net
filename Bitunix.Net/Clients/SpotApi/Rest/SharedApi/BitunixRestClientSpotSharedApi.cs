using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Interfaces.Clients.SpotApi;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixRestClientSpotSharedApi :
        SharedApiBase,
        IBitunixRestClientSpotSharedApi
    {
        private readonly BitunixRestClientSpotApi _api;

        private const string _topicId = "BitunixSpot";
        private const string _exchangeName = "Bitunix";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);

        public BitunixRestClientSpotSharedApi(BitunixRestClientSpotApi api)
            : base(
                  SharedTransport.Rest,
                  api,
                  new[] { TradingMode.Spot },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetDepositHistoryOptions,
                GetDepositAddressesOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetSpotSymbolsOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                CancelSpotOrderOptions,
                GetSpotOrderTradesOptions
                );

        }
    }
}
