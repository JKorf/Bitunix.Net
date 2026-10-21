using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesSharedApi
    {
        #region Get Balance
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures)
        {
            ExchangeParameterRules = [
                new ExchangeParameterDescription(ExchangeParameterRequirement.Optional, "MarginAsset", ["marginCoin"], typeof(string), "The margin asset", "USDT")
                ]
        };

        async Task<IExchangeCallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var balances = await _api.Account.GetBalancesAsync(GetMarginAsset(request), ct: ct).ConfigureAwait(false);
            if (!balances.Success)
                return HttpResult.Fail<SharedBalance[]>(balances);

            return HttpResult.Ok<SharedBalance[]>(balances, [
                new SharedBalance(SupportedTradingModes, balances.Data.MarginAsset, balances.Data.Available, balances.Data.Available + balances.Data.Frozen)]);
        }

        #endregion
    }
}
