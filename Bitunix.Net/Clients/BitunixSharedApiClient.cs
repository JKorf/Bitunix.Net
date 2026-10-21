using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace Bitunix.Net.Clients
{
    internal class BitunixSharedApiClient : SharedApiClientBase, IBitunixSharedApiClient
    {
        /// <inheritdoc />
        public IBitunixRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IBitunixRestClientFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IBitunixSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IBitunixSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixSharedApiClient(
            IBitunixRestClient restClient,
            IBitunixSocketClient socketClient,
            IOptions<BitunixOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                    restClient.SpotApi.SharedApi,
                    restClient.FuturesApi.SharedApi,
                    socketClient.SpotApi.SharedApi,
                    socketClient.FuturesApi.SharedApi
                )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
