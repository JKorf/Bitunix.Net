using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitunixRestClientSpotApiAccount
    {
        /// <summary>
        /// Get user balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#user-interface_1" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/user/account<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset. Only address added under quick withdrawal are allowed. Response is a withdrawal id.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#3-submit-withdrawal-request" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/withdraw<br />
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>network</c>"] Network to use</param>
        /// <param name="address">["<c>address</c>"] Target address</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="memo">["<c>memo</c>"] Memo or tag</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<string>> WithdrawAsync(
            string asset,
            string network,
            string address,
            decimal quantity,
            string memo,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a pending withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#4-cancel-withdrawal" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/withdraw/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="withdrawId">["<c>withdrawId</c>"] Id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> CancelWithdrawalAsync(string withdrawId, CancellationToken ct = default);

        /// <summary>
        /// Transfer between spot and futures account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#8-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/funds_transfer<br />
        /// </para>
        /// </summary>
        /// <param name="type">["<c>type</c>"] Transfer type</param>
        /// <param name="asset">["<c>coin</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<string>> TransferAsync(
            TransferType type,
            string asset,
            decimal quantity,
            CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#7-request-withdrawal-historywithdrawal-and-internal-transfer-included" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/withdraw_transfer/page<br />
        /// </para>
        /// </summary>
        /// <param name="id">["<c>id</c>"] Filter by id</param>
        /// <param name="outBizId">["<c>outBizId</c>"] Filter by external id</param>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="hash">["<c>transactionHash</c>"] Filter by transaction hash</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="remark">["<c>remark</c>"] Filter by remark</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixWithdrawal[]>> GetWithdrawalHistoryAsync(
            string? id = null,
            string? outBizId = null,
            string? asset = null,
            string? hash = null,
            WithdrawType? type = null,
            string? remark = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get deposits
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#9-get-deposit-records-on-chain-deposit-internal-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/deposit/page<br />
        /// </para>
        /// </summary>
        /// <param name="id">["<c>id</c>"] Filter by id</param>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="hash">["<c>transactionHash</c>"] Filter by hash</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixDeposit[]>> GetDepositsAsync(
            string? id = null,
            string? asset = null,
            DepositType? type = null,
            string? hash = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/user/#10-get-deposit-address" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/deposit/address<br />
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>network</c>"] Network to use</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixDepositAddress[]>> GetDepositAddressAsync(
            string asset,
            string network,
            CancellationToken ct = default);

    }
}
