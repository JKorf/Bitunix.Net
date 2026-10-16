using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class BitunixRestClientFuturesApiExchangeData : IBitunixRestClientFuturesApiExchangeData
    {
        private readonly BitunixRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal BitunixRestClientFuturesApiExchangeData(ILogger logger, BitunixRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "XXX", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixModel>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            throw new NotImplementedException();
        }

        #endregion
    }
}
