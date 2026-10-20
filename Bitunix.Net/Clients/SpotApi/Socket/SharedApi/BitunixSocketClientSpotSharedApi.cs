using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Interfaces.Clients.SpotApi;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixSocketClientSpotSharedApi :
        SharedApiBase,
        IBitunixSocketClientSpotSharedApi
    {
        private readonly BitunixSocketClientSpotApi _api;

        private const string _topicId = "BitunixSpot";
        private const string _exchangeName = "Bitunix";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);

        public BitunixSocketClientSpotSharedApi(BitunixSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api,
                  new[] { TradingMode.Spot },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                PlaceSpotOrderOptions,
                CancelSpotOrderOptions
                );
        }
    }
}
