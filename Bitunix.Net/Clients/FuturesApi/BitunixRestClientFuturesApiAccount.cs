using CryptoExchange.Net.Objects;
using Bitunix.Net.Interfaces.Clients.FuturesApi;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class BitunixRestClientFuturesApiAccount : IBitunixRestClientFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitunixRestClientFuturesApi _baseClient;

        internal BitunixRestClientFuturesApiAccount(BitunixRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
