using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
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
        #region Subscribe Kline

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeKlinesSocket.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
            => await SubscribeToKlineUpdatesAsync(request, x => handler(x.ToType<SharedKline>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (FuturesSocketKlineInterval)request.Interval;
            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(
                symbol,
                interval,
                PriceType.LastPrice,
                update => handler(update.ToType(
                    new SharedKline(
                        request.Symbol,
                        update.Symbol!,
                        // Candle doesn't include open time, we need to determine it from the interval and timestamp
                        GetOpenTime(request.Interval, update),
                        update.Data.ClosePrice,
                        update.Data.HighPrice,
                        update.Data.LowPrice,
                        update.Data.OpenPrice,
                        new SharedOrderQuantity(
                            update.Data.BaseVolume,
                            update.Data.QuoteVolume)
                        )
                    {
                    })), ct).ConfigureAwait(false);

            return result;
        }

        private DateTime GetOpenTime(SharedKlineInterval interval, DataEvent<BitunixKlineUpdate> update)
        {
            var time = update.DataTime!.Value;
            if (interval == SharedKlineInterval.OneMonth)
            {
                // For month interval the calculation doesn't work because there isn't a standard length
                return new DateTime(time.Year, time.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            }

            var intervalTicks = ((int)interval) * TimeSpan.TicksPerSecond;
            var left = time.Ticks % intervalTicks;
            var openTime = time.AddTicks(-left);
            return openTime;
        }

        #endregion
    }
}
