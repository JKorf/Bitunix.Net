using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitunixRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#user-interface_1" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/order/place_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderSide">["<c>side</c>"] Side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>volume</c>"] Order quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide orderSide,
            OrderType orderType,
            decimal quantity,
            decimal? price = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#3-cancel-order-batch-cancel" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/order/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#3-cancel-order-batch-cancel" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/order/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="orders">The orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> CancelOrdersAsync(IEnumerable<BitunixCancelOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#5-query-order-history" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/order/history/page<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
        /// <param name="type">["<c>type</c>"] Filter by order type</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Max number of results per page</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPage<BitunixOrder>>> GetOrderHistoryAsync(
            string symbol,
            OrderSide? side = null,
            OrderType? type = null,
            OrderStatus? status = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#6-query-current-orders" /><br />
        /// Endpoint:<br />
        /// POST /api/spot/v1/order/pending/list<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOrder[]>> GetOpenOrdersAsync(
            string symbol,
            CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#7-query-order-detail" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/order/detail<br />
        /// </para>
        /// </summary>
        /// <param name="id">["<c>orderId</c>"] The id</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOrder>> GetOrderAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get user trades for a specific order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order/#4-query-matching-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/order/deal/list<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The id</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixUserTrade[]>> GetUserTradesAsync(
            string symbol,
            string orderId,
            CancellationToken ct = default);
    }
}
