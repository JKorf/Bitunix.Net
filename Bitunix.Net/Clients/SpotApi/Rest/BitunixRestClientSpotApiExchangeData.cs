using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Enums;

namespace Bitunix.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitunixRestClientSpotApiExchangeData : IBitunixRestClientSpotApiExchangeData
    {
        private readonly BitunixRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal BitunixRestClientSpotApiExchangeData(ILogger logger, BitunixRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Last Price

        /// <inheritdoc />
        public async Task<HttpResult<decimal>> GetLastPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/market/last_price", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<decimal>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/market/depth", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<BitunixKline[]>> GetKlinesAsync(
            string symbol,
            KlineInterval interval,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/market/kline", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Kline History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixKline[]>> GetKlineHistoryAsync(
            string symbol,
            KlineInterval interval,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("endTime", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/market/kline/history", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<BitunixSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/common/coin_pair/list", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixSymbol[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Exchange Rates

        /// <inheritdoc />
        public async Task<HttpResult<BitunixExchangeRate[]>> GetExchangeRatesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/common/rate/list", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixExchangeRate[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<HttpResult<BitunixAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/common/coin/coin_network/list", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixAsset[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
