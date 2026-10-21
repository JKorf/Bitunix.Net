using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Guards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class BitunixRestClientFuturesApiTrading : IBitunixRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitunixRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal BitunixRestClientFuturesApiTrading(ILogger logger, BitunixRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixId>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            OrderType orderType,
            decimal quantity,
            decimal? price = null,
            TradeSide? tradeSide = null,
            string? positionId = null,
            TimeInForce? timeInForce = null,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? tpPrice = null,
            PriceType? tpPriceType = null,
            OrderType? tpOrderType = null,
            decimal? tpOrderPrice = null,
            decimal? slPrice = null,
            PriceType? slPriceType = null,
            OrderType? slOrderType = null,
            decimal? slOrderPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side == OrderSide.Buy ? "BUY" : "SELL");
            parameters.Add("orderType", orderType == OrderType.Limit ? "LIMIT" : "MARKET");
            parameters.Add("qty", quantity);
            parameters.Add("price", price);
            parameters.Add("tradeSide", tradeSide);
            parameters.Add("positionId", positionId);
            parameters.Add("effect", timeInForce);
            parameters.Add("clientId", clientOrderId);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.Add("tpPrice", tpPrice);
            parameters.Add("tpStopType", tpPriceType);
            parameters.Add("tpOrderType", tpOrderType == OrderType.Limit ? "LIMIT" : "MARKET");
            parameters.Add("tpOrderPrice", tpOrderPrice);
            parameters.Add("slPrice", slPrice);
            parameters.Add("slStopType", slPriceType);
            parameters.Add("slOrderType", slOrderType == OrderType.Limit ? "LIMIT" : "MARKET");
            parameters.Add("slOrderPrice", slOrderPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/place_order", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixId>> EditOrderAsync(
            string? orderId,
            string? clientOrderId,
            decimal quantity,
            decimal? price = null,
            decimal? tpPrice = null,
            PriceType? tpPriceType = null,
            OrderType? tpOrderType = null,
            decimal? tpOrderPrice = null,
            decimal? slPrice = null,
            PriceType? slPriceType = null,
            OrderType? slOrderType = null,
            decimal? slOrderPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("qty", quantity);
            parameters.Add("orderId", orderId);
            parameters.Add("clientId", clientOrderId);
            parameters.Add("price", price);
            parameters.Add("tpPrice", tpPrice);
            parameters.Add("tpStopType", tpPriceType);
            parameters.Add("tpOrderType", tpOrderType);
            parameters.Add("tpOrderPrice", tpOrderPrice);
            parameters.Add("slPrice", slPrice);
            parameters.Add("slStopType", slPriceType);
            parameters.Add("slOrderType", slOrderType);
            parameters.Add("slOrderPrice", slOrderPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/modify_order", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrderAsync(
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var result = await CancelOrdersAsync(symbol, [new BitunixFuturesCancelRequest {
                OrderId = orderId,
                ClientOrderId = clientOrderId
            }]).ConfigureAwait(false);

            if (!result.Success)
                return result;

            if (result.Data.Failed.Length > 0)
            {
                var orderResult = result.Data.Failed[0];
                return HttpResult.Fail(result, new ServerError(orderResult.ErrorCode!.Value, _baseClient.GetErrorInfo(orderResult.ErrorCode.ToString()!, orderResult.ErrorMsg)));
            }

            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitunixCancelResult>> CancelOrdersAsync(
            string symbol,
            IEnumerable<BitunixFuturesCancelRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderList", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/cancel_orders", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixCancelResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitunixCancelResult>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/cancel_all_orders", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixCancelResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close All Positions

        /// <inheritdoc />
        public async Task<HttpResult> CloseAllPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/close_all_position", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPositionId>> ClosePositionAsync(string positionId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("positionId", positionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/trade/flash_close_position", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixPositionId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesOrderPage>> GetOrderHistoryAsync(
            string? symbol = null,
            string? orderId = null,
            string? clientOrderId = null,
            OrderType? orderType = null,
            bool? queryCanceled = null,
            long? subAccountId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("clientId", clientOrderId);
            parameters.Add("type", orderType);
            parameters.Add("queryCanceled", queryCanceled);
            parameters.Add("subAccountId", subAccountId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/trade/get_history_orders", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixFuturesOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesOrder>> GetOrderAsync(
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("clientId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/trade/get_order_detail", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesOrderPage>> GetOpenOrdersAsync(
            string? symbol = null,
            string? orderId = null,
            string? clientOrderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("clientId", clientOrderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTIme", endTime);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/trade/get_pending_orders", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixFuturesOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<BitunixFuturesUserTradePage>> GetUserTradesAsync(
            string? symbol = null,
            string? orderId = null,
            string? positionId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("positionId", positionId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/trade/get_history_trades", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixFuturesUserTradePage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPosition[]>> GetPositionsAsync(
            string? symbol = null,
            string? positionId = null,
            long? subAccountId = null,
            bool? includeSubAccounts = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", positionId);
            parameters.Add("subAccountId", subAccountId);
            parameters.Add("includeSubAccounts", includeSubAccounts);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/position/get_pending_positions", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPositionHistoryPage>> GetPositionHistoryAsync(
            string? symbol = null,
            string? positionId = null,
            long? subAccountId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", positionId);
            parameters.Add("subAccountId", subAccountId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/position/get_history_positions", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixPositionHistoryPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixId>> PlaceTpSlOrderAsync(
            string symbol,
            string positionId,
            decimal? tpPrice = null,
            PriceType? tpPriceType = null,
            OrderType? tpOrderType = null,
            decimal? tpOrderPrice = null,
            decimal? tpQuantity = null,
            decimal? slPrice = null,
            PriceType? slPriceType = null,
            OrderType? slOrderType = null,
            decimal? slOrderPrice = null,
            decimal? slQuantity = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", positionId);
            parameters.Add("tpPrice", tpPrice);
            parameters.Add("tpStopType", tpPriceType);
            parameters.Add("tpOrderType", tpOrderType);
            parameters.Add("tpOrderPrice", tpOrderPrice);
            parameters.Add("tpQty", tpQuantity);
            parameters.Add("slPrice", slPrice);
            parameters.Add("slStopType", slPriceType);
            parameters.Add("slOrderType", slOrderType);
            parameters.Add("slOrderPrice", slOrderPrice);
            parameters.Add("slQty", slQuantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/tpsl/place_order", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Position Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixId>> PlacePositionTpSlOrderAsync(
            string symbol,
            string positionId,
            decimal? tpPrice = null,
            PriceType? tpPriceType = null,
            decimal? slPrice = null,
            PriceType? slPriceType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", positionId);
            parameters.Add("tpPrice", tpPrice);
            parameters.Add("tpStopType", tpPriceType);
            parameters.Add("slPrice", slPrice);
            parameters.Add("slStopType", slPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/tpsl/position/place_order", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixId>> CancelTpSlOrderAsync(
            string symbol,
            string orderId,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/tpsl/cancel_order", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOpenTpSlOrder[]>> GetOpenTpSlOrdersAsync(
            string symbol,
            string id,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", id);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/tpsl/get_pending_orders", BitunixExchange.RateLimiter.Bitunix, 1, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixOpenTpSlOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tp Sl Order History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOpenTpSlOrder[]>> GetTpSlOrderHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default)
        {
#warning check what is returned

            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("skip", skip);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/tpsl/get_history_orders", BitunixExchange.RateLimiter.Bitunix, 1, true, 
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitunixOpenTpSlOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
