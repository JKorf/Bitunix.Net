using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesSharedApi
    {
        #region Get Leverage

        async Task<IExchangeCallResult<SharedLeverage>> IGetLeverage.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
            => await GetLeverageAsync(request, ct).ConfigureAwait(false);

        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                new ExchangeParameterDescription(ExchangeParameterRequirement.Optional, "MarginAsset", ["marginCoin"], typeof(string), "The margin asset", "USDT")
                ]
        };
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetLeverageMarginModeAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                GetMarginAsset(request),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.Leverage));
        }

        #endregion

        #region Set Leverage

        async Task<IExchangeCallResult<SharedLeverage>> ISetLeverage.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
            => await SetLeverageAsync(request, ct).ConfigureAwait(false);

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            ExchangeParameterRules = [
                new ExchangeParameterDescription(ExchangeParameterRequirement.Optional, "MarginAsset", ["marginCoin"], typeof(string), "The margin asset", "USDT")
                ]
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetLeverageAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol), 
                GetMarginAsset(request), 
                (int)request.Leverage,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(request.Leverage));
        }

        #endregion

        #region Get Leverage Tiers

        async Task<IExchangeCallResult<SharedLeverageTier[]>> IGetLeverageTiers.GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
            => await GetLeverageTiersAsync(request, ct).ConfigureAwait(false);

        public GetLeverageTiersOptions GetLeverageTiersOptions { get; } = new GetLeverageTiersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverageTier[]>> GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageTiersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverageTier[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetPositionTiersAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverageTier[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedLeverageTier(request.Symbol, x.Symbol, x.Level, null, x.StartValue, x.EndValue, x.MaintenanceMarginRate, x.Leverage)
            ).ToArray());
        }

        #endregion
    }
}
