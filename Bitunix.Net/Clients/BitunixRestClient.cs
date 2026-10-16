using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Clients.SpotApi;
using Bitunix.Net.Clients.FuturesApi;

namespace Bitunix.Net.Clients
{
    /// <inheritdoc cref="IBitunixRestClient" />
    public class BitunixRestClient : BaseRestClient<BitunixEnvironment, BitunixCredentials>, IBitunixRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IBitunixRestClientFuturesApi FuturesApi { get; }

         /// <inheritdoc />
        public IBitunixRestClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the BitunixRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitunixRestClient(Action<BitunixRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the BitunixRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BitunixRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BitunixRestOptions> options) : base(loggerFactory, "Bitunix")
        {
            Initialize(options.Value);

            FuturesApi = AddApiClient(new BitunixRestClientFuturesApi(loggerFactory, httpClient, options.Value));
            SpotApi = AddApiClient(new BitunixRestClientSpotApi(loggerFactory, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitunixRestOptions> optionsDelegate)
        {
            BitunixRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
    }
}
