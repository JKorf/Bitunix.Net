using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net;
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
        #region Subscribe Futures Orders

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToUserUpdatesAsync(
                onOrderMessage: update => {

                    handler(update.ToType<SharedFuturesOrderUpdate[]>(
                        [new SharedFuturesOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol,
                            update.Data.OrderId,
                            update.Data.Type == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                            update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(update.Data),
                            update.Data.CreateTime
                            )
                        {
                            AveragePrice = update.Data.AveragePrice,
                            ClientOrderId = update.Data.ClientOrderId,
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = update.Data.Fee,
#pragma warning restore CS0618 // Type or member is obsolete
                            Leverage = update.Data.Leverage,
                            OrderPrice = update.Data.Price,
                            OrderQuantity = new SharedOrderQuantity(update.Data.Quantity),
                            QuantityFilled = new SharedOrderQuantity(update.Data.QuantityFilled),
                            TimeInForce = ParseTimeInForce(update.Data.TimeInForce),
                            UpdateTime = update.Data.UpdateTime
                        }]));
                }, ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? timeInForce)
        {
            if (timeInForce == TimeInForce.FillOrKill)
                return SharedTimeInForce.FillOrKill;
            if (timeInForce == TimeInForce.GoodTillCanceled)
                return SharedTimeInForce.GoodTillCanceled;
            if (timeInForce == TimeInForce.ImmediateOrCancel)
                return SharedTimeInForce.ImmediateOrCancel;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(BitunixFuturesOrderUpdate data)
        {
            if (data.Status == FuturesOrderStatus.Filled)
                return SharedOrderStatus.Filled;
            if (data.Status == FuturesOrderStatus.Canceled)
                return SharedOrderStatus.Canceled;
            if (data.Status == FuturesOrderStatus.New
                || data.Status == FuturesOrderStatus.Initial
                || data.Status == FuturesOrderStatus.PartiallyFilled)
            {
                return SharedOrderStatus.Open;
            }

            return SharedOrderStatus.Unknown;
        }
        #endregion
    }
}
