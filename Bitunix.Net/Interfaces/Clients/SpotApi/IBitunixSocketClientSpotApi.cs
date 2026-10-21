using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Interfaces.Clients;
using Bitunix.Net.Enums;
using System.Collections.Generic;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot Socket operations
    /// </summary>
    public interface IBitunixSocketClientSpotApi : ISocketApiClient<BitunixCredentials>, IDisposable
    {
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBitunixSocketClientSpotSharedApi SharedApi { get; }

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order-ws/#1-place-order" /><br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderSide">["<c>side</c>"] Side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>volume</c>"] Order quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult<BitunixOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide orderSide,
            OrderType orderType,
            decimal quantity,
            decimal price,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order-ws/#3-cancel-order-batch-cancel" /><br />
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/order-ws/#3-cancel-order-batch-cancel" /><br />
        /// </para>
        /// </summary>
        /// <param name="orders">The orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult> CancelOrdersAsync(IEnumerable<BitunixCancelOrderRequest> orders, CancellationToken ct = default);
    }
}
