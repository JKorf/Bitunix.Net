using Bitunix.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixSocketClientFuturesSharedApi :
        SharedApiBase,
        IBitunixSocketClientFuturesSharedApi
    {
        private readonly BitunixSocketClientFuturesApi _api;

        private const string _topicId = "BitunixFutures";
        private const string _exchangeName = "Bitunix";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);

        public BitunixSocketClientFuturesSharedApi(BitunixSocketClientFuturesApi api)
            : base(
                  SharedTransport.Socket,
                  api,
                  new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTradeOptions,
                SubscribeTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeFundingInfoOptions,
                SubscribeMarkPriceOptions,
                SubscribeIndexPriceOptions,
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions
                );
        }

        /// <inheritdoc />
        public Task UnsubscribeAllAsync() => _api.UnsubscribeAllAsync();
    }
}
