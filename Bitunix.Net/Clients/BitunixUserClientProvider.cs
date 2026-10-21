using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using CryptoExchange.Net.Clients;

namespace Bitunix.Net.Clients
{
    /// <inheritdoc />
    public class BitunixUserClientProvider : UserClientProvider<
        IBitunixRestClient,
        IBitunixSocketClient,
        BitunixRestOptions,
        BitunixSocketOptions,
        BitunixCredentials,
        BitunixEnvironment
        >, IBitunixUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => BitunixExchange.Metadata.Id;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public BitunixUserClientProvider(Action<BitunixOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<BitunixRestOptions> restOptions,
            IOptions<BitunixSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IBitunixRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<BitunixRestOptions> options)
            => new BitunixRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IBitunixSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<BitunixSocketOptions> options)
            => new BitunixSocketClient(options, loggerFactory);
    }
}
