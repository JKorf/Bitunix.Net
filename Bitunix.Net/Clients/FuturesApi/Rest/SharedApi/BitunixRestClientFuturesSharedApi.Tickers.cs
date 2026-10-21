using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesSharedApi
    {
        #region Get Tickers

        async Task<IExchangeCallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTicker)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName, SharedTickerType.Day24H);
        public async Task<HttpResult<SharedTicker>> GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(
                [request.Symbol!.GetSymbol(FormatSymbol)],
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedTicker>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, ticker.Symbol),
                    ticker.Symbol,
                    ticker.LastPrice,
                    ticker.HighPrice,
                    ticker.LowPrice,
                    new SharedOrderQuantity(ticker.BaseVolume, ticker.QuoteVolume),
                    ticker.OpenPrice > 0 && ticker.LastPrice > 0 ? Math.Round(ticker.LastPrice / ticker.OpenPrice * 100 - 100, 4) : null));
        }

        #endregion

        #region Get All Tickers

        async Task<IExchangeCallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickers)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName, SharedTickerType.Day24H);
        public async Task<HttpResult<SharedTicker[]>> GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => new SharedTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.BaseVolume, x.QuoteVolume),
                    x.OpenPrice > 0 && x.LastPrice > 0 ? Math.Round(x.LastPrice / x.OpenPrice * 100 - 100, 4) : null)
            ).ToArray());
        }

        #endregion
    }
}
