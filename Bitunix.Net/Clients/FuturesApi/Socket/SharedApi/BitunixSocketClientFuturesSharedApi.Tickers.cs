using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixSocketClientFuturesSharedApi
    {
        #region Subscribe Tickers

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName, SharedTickerType.Day24H);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(
                symbol,
                update => handler(update.ToType(
                     new SharedTicker(
                        request.Symbol,
                        update.Symbol!,
                        update.Data.LastPrice,
                        update.Data.HighPrice,
                        update.Data.LowPrice,
                        new SharedOrderQuantity(update.Data.VolumeBase, update.Data.VolumeQuote),
                        update.Data.PriceChangePercentage
                        )
                )
            )
            , ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
