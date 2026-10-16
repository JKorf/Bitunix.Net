using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitunixRestClientSpotApiTrading : IBitunixRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitunixRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal BitunixRestClientSpotApiTrading(ILogger logger, BitunixRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide orderSide,
            OrderType orderType,
            decimal quantity,
            decimal price,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("volume", quantity);
            parameters.Add("price", price);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/order/place_order", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public Task<HttpResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default)
            => CancelOrdersAsync([new BitunixCancelOrderRequest { Symbol = symbol, OrderId = orderId }], ct);

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrdersAsync(IEnumerable<BitunixCancelOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("orderIdList", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/order/cancel", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<BitunixPage<BitunixOrder>>> GetOrderHistoryAsync(
            string symbol,
            OrderSide? side = null,
            OrderType? type = null,
            OrderStatus? status = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("status", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/order/history/page", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixPage<BitunixOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOrder[]>> GetOpenOrdersAsync(
            string symbol,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/order/pending/list", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<BitunixOrder>> GetOrderAsync(string id, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("orderId", id);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/spot/v1/order/detail", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<BitunixUserTrade[]>> GetUserTradesAsync(
            string symbol,
            string orderId,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/spot/v1/order/pending/list", BitunixExchange.RateLimiter.Bitunix, 1, true);
            var result = await _baseClient.SendAsync<BitunixUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
