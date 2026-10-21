using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixRestClientSpotSharedApi
	{

		#region Get All Assets
		public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);

		async Task<IExchangeCallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
			=> await GetAllAssetsAsync(request, ct).ConfigureAwait(false);

		public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
		{
			var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
			if (validationError != null)
				return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

			var assets = await _api.ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
			if (!assets.Success)
				return HttpResult.Fail<SharedAsset[]>(assets);

			return HttpResult.Ok(assets, assets.Data.Select(x => new SharedAsset(x.Name)
			{
				Networks = x.Networks.Select(x => new SharedAssetNetwork(x.NetworkName)
				{
					MinConfirmations = x.DepositConfirm,
					DepositEnabled = x.DepositOpen,
					MinWithdrawQuantity = x.WithdrawMin,
					WithdrawEnabled = x.WithdrawOpen,
					ContractAddress = x.ContractAddress,
					MaxWithdrawQuantity = x.WithdrawMax
				}).ToArray()
			}).ToArray());
		}

		#endregion

	}
}
