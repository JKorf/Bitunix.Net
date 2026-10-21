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
        #region Subscribe Index Price

        public SubscribeIndexPriceOptions SubscribeIndexPriceOptions { get; } = new SubscribeIndexPriceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(SubscribeIndexPriceRequest request, Action<DataEvent<SharedIndexPrice>> handler, CancellationToken ct)
        {
            var validationError = SubscribeIndexPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToMarkPriceUpdatesAsync(
                symbol,
                update => handler(update.ToType(

                    new SharedIndexPrice(
                        request.Symbol,
                        update.Symbol!,
                        update.Data.IndexPrice
                        )

                )
            )
            , ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
