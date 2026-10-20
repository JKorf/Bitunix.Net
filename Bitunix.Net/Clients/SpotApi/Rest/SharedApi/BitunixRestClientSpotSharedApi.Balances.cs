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

		#region Get Balance
		public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot);

		async Task<IExchangeCallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
			=> await GetBalancesAsync(request, ct).ConfigureAwait(false);

		public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
		{
			var validationError = GetBalancesOptions.ValidateRequest(request, this);
			if (validationError != null)
				return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

			var Balances = await _api.Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
			if (!Balances.Success)
				return HttpResult.Fail<SharedBalance[]>(Balances);

			return HttpResult.Ok(Balances, Balances.Data.Select(x => new SharedBalance(SupportedTradingModes, x.Asset, x.Balance, x.Balance + x.BalanceLocked)).ToArray());
		}

		#endregion

	}
}
