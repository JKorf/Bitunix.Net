using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Clients.FuturesApi;
using Bitunix.Net.Clients.SpotApi;

namespace Bitunix.Net.Clients
{
    /// <inheritdoc cref="IBitunixSocketClient" />
    public class BitunixSocketClient : BaseSocketClient<BitunixEnvironment, BitunixCredentials>, IBitunixSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        /// <inheritdoc />
        public IBitunixSocketClientFuturesApi FuturesApi { get; }

         /// <inheritdoc />
        public IBitunixSocketClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BitunixSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitunixSocketClient(Action<BitunixSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of BitunixSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public BitunixSocketClient(IOptions<BitunixSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Bitunix")
        {
            Initialize(options.Value);

            FuturesApi = AddApiClient(new BitunixSocketClientFuturesApi(loggerFactory, options.Value));
            SpotApi = AddApiClient(new BitunixSocketClientSpotApi(loggerFactory, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitunixSocketOptions> optionsDelegate)
        {
            BitunixSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
    }
}
