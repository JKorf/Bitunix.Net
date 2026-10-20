using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Guards;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static CryptoExchange.Net.SharedApis.SharedCapabilities;

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

        #region Adjust Margin

        /// <inheritdoc />
        public async Task<HttpResult> AdjustMarginAsync(
            string symbol,
            string asset,
            decimal quantity,
            PositionSide? side = null,
            string? id = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginCoin", asset);
            parameters.Add("amount", quantity);
            parameters.Add("side", side);
            parameters.Add("positionId", id);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/account/adjust_position_margin", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesBalance>> GetBalancesAsync(string marginAsset, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("marginCoin", marginAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/account", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixFuturesBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Settings

        /// <inheritdoc />
        public async Task<HttpResult<BitunixTradingSetting[]>> GetTradingSettingsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/account/trading_settings", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixTradingSetting[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPositionMode>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/account/position_mode",
                BitunixExchange.RateLimiter.Bitunix, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixPositionMode>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Margin Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitunixLeverageMargin>> GetLeverageMarginModeAsync(
            string symbol,
            string marginAsset,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginCoin", marginAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/account/get_leverage_margin_mode", 
                BitunixExchange.RateLimiter.Bitunix, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixLeverageMargin>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPositionMode>> SetPositionModeAsync(Enums.PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("positionMode", positionMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/account/change_position_mode", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitunixMarginMode>> SetMarginModeAsync(
            string symbol,
            string asset,
            MarginMode mode,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginCoin", asset);
            parameters.Add("marginMode", mode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/account/change_margin_mode", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixMarginMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<BitunixLeverage>> SetLeverageAsync(
            string symbol,
            string marginAsset,
            int leverage,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginCoin", marginAsset);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/account/change_leverage", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
