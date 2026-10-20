using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Guards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesTicker[]>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/tickers", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFuturesTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesOrderBook>> GetOrderBookAsync(
            string symbol,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit?.ToString() ?? "max");
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/depth", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFuturesOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rates

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFundingRate[]>> GetFundingRatesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/funding_rate/batch", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFundingRate[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFundingEntry[]>> GetFundingRateHistoryAsync(
            string symbol,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/get_funding_rate_history", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFundingEntry[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/funding_rate", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesKline[]>> GetKlinesAsync(
            string symbol,
            FuturesKlineInterval interval,
            PriceType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("type", type);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/kline", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesSymbol[]>> GetSymbolsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("symbol", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/market/trading_pairs", BitunixExchange.RateLimiter.Bitunix, 1, false);
            var result = await _baseClient.SendAsync<BitunixFuturesSymbol[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Tiers

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPositionTier[]>> GetPositionTiersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/position/get_position_tiers", BitunixExchange.RateLimiter.Bitunix, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitunixPositionTier[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
