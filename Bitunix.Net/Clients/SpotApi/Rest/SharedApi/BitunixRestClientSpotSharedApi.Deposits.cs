using Bitunix.Net.Enums;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixRestClientSpotSharedApi
	{

        #region Get Deposit Addresses

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<GetDepositAddressesRequest>.Required(x => x.Network)
                ]
        };
        async Task<IExchangeCallResult<SharedDepositAddress[]>> IGetDepositAddresses.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
            => await GetDepositAddressesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressAsync(request.Asset, request.Network!).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Select(x => new SharedDepositAddress(x.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }).ToArray()
            );
        }

        #endregion

        #region Get Deposit
        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 100);

		async Task<IExchangeCallResult<SharedDeposit[]>> IGetDepositHistory.GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
			=> await GetDepositHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

		public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
		{
			var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
			if (validationError != null)
				return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true, TimeSpan.FromDays(90));

            var result = await _api.Account.GetDepositsAsync(
				asset: request.Asset,
				startTime: pageParams.StartTime,
				endTime: pageParams.EndTime,
				limit: limit,
				ct: ct).ConfigureAwait(false);
			if (!result.Success)
				return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => Pagination.NextPageFromTime(pageParams,  result.Data.Min(x => x.CreateTime)),
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, 
                       ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Where(x => x.Type == DepositType.Deposit)
                       .Select(x =>
                           new SharedDeposit(
                               x.Asset,
                               x.Quantity,
                               x.Status != Enums.WithdrawStatus.Pending && x.Status != Enums.WithdrawStatus.PendingVerification,
                               x.CreateTime,
                               ParseTransferStatus(x.Status))
                           {
                               Id = x.Id.ToString(),
                               Network = x.Network,
                               TransactionId = x.TransactionHash
                           })
                      .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(WithdrawStatus status)
        {
            if (status == WithdrawStatus.Canceled || status == WithdrawStatus.Failed)
                return SharedTransferStatus.Failed;

            if (status == WithdrawStatus.Success)
                return SharedTransferStatus.Completed;

            if (status == WithdrawStatus.Pending || status == WithdrawStatus.PendingVerification)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

    }
}
