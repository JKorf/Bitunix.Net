using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixRestClientSpotSharedApi
    {
        #region Get Withdrawal History

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 100);

        async Task<IExchangeCallResult<SharedWithdrawal[]>> IGetWithdrawalHistory.GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetWithdrawalHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true);

            // Get data
            var time = DateTime.UtcNow;
            var result = await _api.Account.GetWithdrawalHistoryAsync(
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                asset: request.Asset,
                type: WithdrawType.Withdrawal,
                limit: limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x =>
                           new SharedWithdrawal(
                               x.Asset,
                               x.Address,
                               x.Quantity,
                               x.Status != WithdrawStatus.Pending && x.Status != WithdrawStatus.PendingVerification,
                               x.CreateTime,
                               GetWithdrawalStatus(x))
                           {
                               Id = x.Id,
                               Network = x.Network,
                               Tag = x.Memo,
                               TransactionId = x.TransactionHash,
                               Fee = x.Fee,
                           })
                       .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus GetWithdrawalStatus(BitunixWithdrawal x)
        {
            if (x.Status == WithdrawStatus.Failed)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawStatus.Success)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawStatus.Pending || x.Status == WithdrawStatus.PendingVerification)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #region Withdraw

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<WithdrawRequest>.Required(x => x.Network)
                ]
        };

        async Task<IExchangeCallResult<SharedId>> IWithdraw.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
            => await WithdrawAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Network!,
                request.Address,
                request.Quantity,
                request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data));
        }

        #endregion

    }
}
