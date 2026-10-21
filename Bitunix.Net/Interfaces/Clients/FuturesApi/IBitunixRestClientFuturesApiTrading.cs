using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitunixRestClientFuturesApiTrading
    {
        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/place_order.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/place_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>orderType</c>"] Order type</param>
        /// <param name="quantity">["<c>qty</c>"] Quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="tradeSide">["<c>tradeSide</c>"] Trade side, required in hedge mode</param>
        /// <param name="positionId">["<c>positionId</c>"] Position id, for closing position</param>
        /// <param name="timeInForce">["<c>effect</c>"] Time in force</param>
        /// <param name="clientOrderId">["<c>clientId</c>"] Client order id</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only</param>
        /// <param name="tpPrice">["<c>tpPrice</c>"] Take profit price</param>
        /// <param name="tpPriceType">["<c>tpStopType</c>"] Take profit price type</param>
        /// <param name="tpOrderType">["<c>tpOrderType</c>"] Take profit order type</param>
        /// <param name="tpOrderPrice">["<c>tpOrderPrice</c>"] Take profit limit price</param>
        /// <param name="slPrice">["<c>slPrice</c>"] Stop loss price</param>
        /// <param name="slPriceType">["<c>slStopType</c>"] Stop loss price type</param>
        /// <param name="slOrderType">["<c>slOrderType</c>"] Stop loss order type</param>
        /// <param name="slOrderPrice">["<c>slOrderPrice</c>"] Stop loss limit price</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixId>> PlaceOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Edit an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/modify_order.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/modify_order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="quantity">["<c>qty</c>"] Quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="tpPrice">["<c>tpPrice</c>"] Take profit trigger price</param>
        /// <param name="tpPriceType">["<c>tpStopType</c>"] Take profit price type</param>
        /// <param name="tpOrderType">["<c>tpOrderType</c>"] Take profit order type</param>
        /// <param name="tpOrderPrice">["<c>tpOrderPrice</c>"] Take profit order price</param>
        /// <param name="slPrice">["<c>slPrice</c>"] Stop loss trigger price</param>
        /// <param name="slPriceType">["<c>slStopType</c>"] Stop loss price type</param>
        /// <param name="slOrderType">["<c>slOrderType</c>"] Stop loss order type</param>
        /// <param name="slOrderPrice">["<c>slOrderPrice</c>"] Stop loss order price</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixId>> EditOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/cancel_orders.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/cancel_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> CancelOrderAsync(
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/cancel_orders.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/cancel_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orders">["<c>orderList</c>"] Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixCancelResult>> CancelOrdersAsync(
            string symbol,
            IEnumerable<BitunixFuturesCancelRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/cancel_all_orders.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/cancel_all_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixCancelResult>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Close all positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/close_all_position.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/close_all_position<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> CloseAllPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Close a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/flash_close_position.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/trade/flash_close_position<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPositionId>> ClosePositionAsync(string positionId, CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/get_history_orders.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/trade/get_history_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="clientOrderId">["<c>clientId</c>"] Filter by client order id</param>
        /// <param name="orderType">["<c>type</c>"] Filter by order type</param>
        /// <param name="queryCanceled">["<c>queryCanceled</c>"] If true only returns canceled orders, if false canceled orders are not returned</param>
        /// <param name="subAccountId">["<c>subAccountId</c>"] Filter by sub account id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="skip">["<c>skip</c>"] Skip results</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesOrderPage>> GetOrderHistoryAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/get_order_detail.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/trade/get_order_detail<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesOrder>> GetOrderAsync(
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/get_pending_orders.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/trade/get_pending_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="clientOrderId">["<c>clientId</c>"] Filter by client order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTIme</c>"] Filter by end time</param>
        /// <param name="skip">["<c>skip</c>"] Skip rows</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesOrderPage>> GetOpenOrdersAsync(
            string? symbol = null,
            string? orderId = null,
            string? clientOrderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/trade/get_history_trades.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/trade/get_history_trades<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="positionId">["<c>positionId</c>"] Filter by position id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="skip">["<c>skip</c>"] Skip rows</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesUserTradePage>> GetUserTradesAsync(
            string? symbol = null,
            string? orderId = null,
            string? positionId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/position/get_pending_positions.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/position/get_pending_positions<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="positionId">["<c>positionId</c>"] Filter by id</param>
        /// <param name="subAccountId">["<c>subAccountId</c>"] Filter by sub account id</param>
        /// <param name="includeSubAccounts">["<c>includeSubAccounts</c>"] Whether to include sub account</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPosition[]>> GetPositionsAsync(
            string? symbol = null,
            string? positionId = null,
            long? subAccountId = null,
            bool? includeSubAccounts = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/position/get_history_positions.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/position/get_history_positions<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="positionId">["<c>positionId</c>"] Filter by position id</param>
        /// <param name="subAccountId">["<c>subAccountId</c>"] Filter by sub account id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="skip">["<c>skip</c>"] Skip rows</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPositionHistoryPage>> GetPositionHistoryAsync(
            string? symbol = null,
            string? positionId = null,
            long? subAccountId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new take profit / stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/tp_sl/place_tp_sl_order.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/tpsl/place_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="positionId">["<c>positionId</c>"] The position id</param>
        /// <param name="tpPrice">["<c>tpPrice</c>"] Take profit price</param>
        /// <param name="tpPriceType">["<c>tpStopType</c>"] Take profit price type</param>
        /// <param name="tpOrderType">["<c>tpOrderType</c>"] Take profit order type</param>
        /// <param name="tpOrderPrice">["<c>tpOrderPrice</c>"] Take profit limit order price</param>
        /// <param name="tpQuantity">["<c>tpQty</c>"] Take profit quantity</param>
        /// <param name="slPrice">["<c>slPrice</c>"] Stop loss price</param>
        /// <param name="slPriceType">["<c>slStopType</c>"] Stop loss price type</param>
        /// <param name="slOrderType">["<c>slOrderType</c>"] Stop loss order type</param>
        /// <param name="slOrderPrice">["<c>slOrderPrice</c>"] Stop loss limit order price</param>
        /// <param name="slQuantity">["<c>slQty</c>"] Stop loss quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixId>> PlaceTpSlOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Place position take profit/stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/tp_sl/place_position_tp_sl_order.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/tpsl/position/place_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="positionId">["<c>positionId</c>"] The position id</param>
        /// <param name="tpPrice">["<c>tpPrice</c>"] Take profit price</param>
        /// <param name="tpPriceType">["<c>tpStopType</c>"] Price type</param>
        /// <param name="slPrice">["<c>slPrice</c>"] Stop loss price</param>
        /// <param name="slPriceType">["<c>slStopType</c>"] Price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixId>> PlacePositionTpSlOrderAsync(
            string symbol,
            string positionId,
            decimal? tpPrice = null,
            PriceType? tpPriceType = null,
            decimal? slPrice = null,
            PriceType? slPriceType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a take profit / stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/tp_sl/cancel_tp_sl_order.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/tpsl/cancel_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixId>> CancelTpSlOrderAsync(
            string symbol,
            string orderId,
            CancellationToken ct = default);

        /// <summary>
        /// Get open take profit / stop loss orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/tp_sl/get_pending_tp_sl_order.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/tpsl/get_pending_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="id">["<c>positionId</c>"] The id</param>
        /// <param name="skip">["<c>skip</c>"] Skip rows</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOpenTpSlOrder[]>> GetOpenTpSlOrdersAsync(
            string symbol,
            string id,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get tp/sl order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/tp_sl/get_history_tp_sl_order.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/tpsl/get_history_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="skip">["<c>skip</c>"] Skip rows</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOpenTpSlOrder[]>> GetTpSlOrderHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? skip = null,
            int? limit = null,
            CancellationToken ct = default);

    }
}
