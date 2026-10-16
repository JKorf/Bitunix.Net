using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Interfaces.Clients.FuturesApi;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class BitunixRestClientFuturesApiTrading : IBitunixRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitunixRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal BitunixRestClientFuturesApiTrading(ILogger logger, BitunixRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
