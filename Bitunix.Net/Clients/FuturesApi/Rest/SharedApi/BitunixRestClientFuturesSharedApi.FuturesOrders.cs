using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesSharedApi
    {

        #region Place Futures Order

        async Task<IExchangeCallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, true)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<PlaceFuturesOrderRequest>.Required(x => x.PositionSide),
                RequestParameterRuleOverride<PlaceFuturesOrderRequest>.NotSupported(x => x.Leverage),
                RequestParameterRuleOverride<PlaceFuturesOrderRequest>.NotSupported(x => x.MarginMode),
                ]
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Market ? OrderType.Market : OrderType.Limit,
                request.Quantity?.QuantityInBaseAsset ?? 0,
                request.Price,
                timeInForce: GetTimeInForce(request),
                reduceOnly: request.ReduceOnly,
                tpPrice: request.TakeProfitPrice,
                slPrice: request.StopLossPrice,
                tradeSide: GetTradeSide(request),
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        private TradeSide? GetTradeSide(PlaceFuturesOrderRequest request)
        {
            if ((request.PositionSide == SharedPositionSide.Long && request.Side == SharedOrderSide.Buy)
                || (request.PositionSide == SharedPositionSide.Short && request.Side == SharedOrderSide.Sell))
            {
                return TradeSide.Open;
            }

            return TradeSide.Close;
        }

        private TimeInForce? GetTimeInForce(PlaceFuturesOrderRequest request)
        {
            if (request.OrderType == SharedOrderType.LimitMaker)
                return TimeInForce.PostOnly;

            if (request.TimeInForce == null)
                return null;

            if (request.TimeInForce == SharedTimeInForce.FillOrKill)
                return TimeInForce.FillOrKill;

            if (request.TimeInForce == SharedTimeInForce.ImmediateOrCancel)
                return TimeInForce.ImmediateOrCancel;

            return TimeInForce.GoodTillCanceled;
        }

        #endregion

        #region Get Futures Order

        async Task<IExchangeCallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data),
                order.Data.CreateTime)
            {
                AveragePrice = order.Data.AveragePrice,
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                TakeProfitPrice = order.Data.TpPrice,
                StopLossPrice = order.Data.SlPrice,
                ReduceOnly = order.Data.ReduceOnly
            });
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

        private SharedOrderStatus ParseOrderStatus(BitunixFuturesOrder data)
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

        private SharedOrderType ParseOrderType(BitunixFuturesOrder data)
        {
            if (data.Type == OrderType.Market)
                return SharedOrderType.Market;

            if (data.TimeInForce == TimeInForce.PostOnly)
                return SharedOrderType.LimitMaker;

            return SharedOrderType.Limit;
        }

        #endregion


        #region Get Futures Order By Client Order Id

        async Task<IExchangeCallResult<SharedFuturesOrder>> IGetFuturesOrderByClientOrderId.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(clientOrderId: request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data),
                order.Data.CreateTime)
            {
                AveragePrice = order.Data.AveragePrice,
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                TakeProfitPrice = order.Data.TpPrice,
                StopLossPrice = order.Data.SlPrice,
                ReduceOnly = order.Data.ReduceOnly
            });
        }

        #endregion

        #region Get Open Futures Orders

        async Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var order = await _api.Trading.GetOpenOrdersAsync(
                request.Symbol?.GetSymbol(FormatSymbol)).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(order);

            return HttpResult.Ok(order, order.Data.Orders.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x),
                x.CreateTime)
            {
                AveragePrice = x.AveragePrice,
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(x.Quantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                TakeProfitPrice = x.TpPrice,
                StopLossPrice = x.SlPrice,
                ReduceOnly = x.ReduceOnly
            }).ToArray());
        }

        #endregion

        #region Get Closed Futures Orders

        async Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(90),
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("QueryCanceled", "Whether to query canceled orders. If true only canceled orders will be returned, if false canceled orders will be ommited", true)
                ]
        };
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var queryCanceled = request.GetParamValue<bool?>("QueryCanceled");
            // Get data
            var result = await _api.Trading.GetOrderHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                skip: pageParams.Offset,
                limit: pageParams.Limit,
                queryCanceled: queryCanceled == true,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                        () => Pagination.NextPageFromOffset(pageParams, result.Data.Orders.Length),
                        result.Data.Orders.Length,
                        result.Data.Orders.Select(x => x.CreateTime),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams);

            return HttpResult.Ok(result, result.Data.Orders.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x),
                x.CreateTime)
            {
                AveragePrice = x.AveragePrice,
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(x.Quantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                TakeProfitPrice = x.TpPrice,
                StopLossPrice = x.SlPrice,
                ReduceOnly = x.ReduceOnly
            }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel Futures Order

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel Futures Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                clientOrderId: request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Get Futures Order Trades

        async Task<IExchangeCallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await _api.Trading.GetUserTradesAsync(
                symbol,
                orderId: request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedUserTrade[]>(order);

            return HttpResult.Ok(order, order.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantity),
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        #endregion

        #region Get Futures User Trade History

        async Task<IExchangeCallResult<SharedUserTrade[]>> IGetFuturesUserTradeHistory.GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFuturesUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                skip: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                        () => Pagination.NextPageFromOffset(pageParams, result.Data.Trades.Length),
                        result.Data.Trades.Length,
                        result.Data.Trades.Select(x => x.CreateTime),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams);

            return HttpResult.Ok(result, result.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantity),
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel All Futures Symbol Orders

        async Task<IExchangeCallResult> ICancelAllFuturesSymbolOrders.CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesSymbolOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesSymbolOrdersOptions CancelAllFuturesSymbolOrdersOptions { get; } = new CancelAllFuturesSymbolOrdersOptions(_exchangeName, true);
        public async Task<HttpResult> CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesSymbolOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail(Exchange, validationError);

            var order = await _api.Trading.CancelAllOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail(order);

            return HttpResult.Ok(order);
        }

        #endregion

        #region Cancel All Futures Symbol Orders

        async Task<IExchangeCallResult> ICancelAllFuturesOrders.CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesOrdersOptions CancelAllFuturesOrdersOptions { get; } = new CancelAllFuturesOrdersOptions(_exchangeName);
        public async Task<HttpResult> CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail(Exchange, validationError);

            var order = await _api.Trading.CancelAllOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail(order);

            return HttpResult.Ok(order);
        }

        #endregion

        #region Edit Futures Order

        async Task<IExchangeCallResult<SharedId>> IEditFuturesOrder.EditFuturesOrderAsync(EditOrderRequest request, CancellationToken ct)
            => await EditFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public EditFuturesOrderOptions EditFuturesOrderOptions { get; } = new EditFuturesOrderOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> EditFuturesOrderAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.EditOrderAsync(
                request.OrderId,
                null,
                quantity: request.Quantity?.QuantityInBaseAsset ?? 0,
                price: request.Price,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion

        #region Edit Futures Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> IEditFuturesOrderByClientOrderId.EditFuturesOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
            => await EditFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public EditFuturesOrderByClientOrderIdOptions EditFuturesOrderByClientOrderIdOptions { get; } = new EditFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> EditFuturesOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.EditOrderAsync(
                null,
                clientOrderId: request.OrderId,
                quantity: request.Quantity?.QuantityInBaseAsset ?? 0,
                price: request.Price,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion
    }
}
