using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
#warning TODO
                null,
                null,
                null,
                null,
                null,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig(),
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
#warning TODO
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig(),
                null,
                exchangeParameters)
        {

        }
    }
}
