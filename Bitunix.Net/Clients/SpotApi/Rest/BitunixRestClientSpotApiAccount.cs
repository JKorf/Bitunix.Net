using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitunixRestClientSpotApiAccount : IBitunixRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitunixRestClientSpotApi _baseClient;

        internal BitunixRestClientSpotApiAccount(BitunixRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<BitunixBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/user/account", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixBalance[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<string>> WithdrawAsync(
            string asset,
            string network,
            string address,
            decimal quantity,
            string? memo = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("coin", asset);
            parameters.Add("network", network);
            parameters.Add("address", address);
            parameters.Add("amount", quantity);
            parameters.Add("memo", memo);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/withdraw", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<string>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<HttpResult> CancelWithdrawalAsync(string withdrawId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("withdrawId", withdrawId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/withdraw/cancel", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<string>> TransferAsync(
            TransferType type,
            string asset,
            decimal quantity,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("type", type);
            parameters.Add("coin", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/funds_transfer", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<string>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixWithdrawal[]>> GetWithdrawalHistoryAsync(
            string? id = null,
            string? outBizId = null,
            string? asset = null,
            string? hash = null,
            WithdrawType? type = null,
            string? remark = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            parameters.Add("outBizId", outBizId);
            parameters.Add("coin", asset);
            parameters.Add("transactionHash", hash);
            parameters.Add("type", type);
            parameters.Add("remark", remark);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/withdraw_transfer/page", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<HttpResult<BitunixDeposit[]>> GetDepositsAsync(
            string? id = null,
            string? asset = null,
            DepositType? type = null,
            string? hash = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            parameters.Add("coin", asset);
            parameters.Add("type", type);
            parameters.Add("transactionHash", hash);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/deposit/page", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixDeposit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<BitunixDepositAddress[]>> GetDepositAddressAsync(
            string asset,
            string network,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("coin", asset);
            parameters.Add("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/deposit/address", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
