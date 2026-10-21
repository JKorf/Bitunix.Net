using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Interfaces.Clients;

namespace Bitunix.Net
{
    /// <inheritdoc />
    public class BitunixUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BitunixUserSpotDataTracker(
            ILogger<BitunixUserSpotDataTracker> logger,
            IBitunixRestClient restClient,
            IBitunixSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config = null,
            ExchangeParameters? exchangeParameters = null) : base(
                logger,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                null,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                null,

                null,
                null,
                userIdentifier,
                (config ?? new SpotUserDataTrackerConfig()) with { TrackTrades = false },
                exchangeParameters)
        {

        }
    }

    /// <inheritdoc />
    public class BitunixUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixUserFuturesDataTracker(
            ILogger<BitunixUserFuturesDataTracker> logger,
            IBitunixRestClient restClient,
            IBitunixSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config = null,
            ExchangeParameters? exchangeParameters = null) :
            base(logger,
                restClient.FuturesApi.SharedApi,
                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.FuturesApi.SharedApi,
                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.FuturesApi.SharedApi,
                null,

                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig(),
                null,
                exchangeParameters)
        {

        }
    }
}
