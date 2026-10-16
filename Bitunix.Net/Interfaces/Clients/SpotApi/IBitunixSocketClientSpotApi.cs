using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Interfaces.Clients;
using Bitunix.Net.Enums;
using System.Collections.Generic;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot streams
    /// </summary>
    public interface IBitunixSocketClientSpotApi : ISocketApiClient<BitunixCredentials>, IDisposable
    {

        Task<QueryResult<BitunixOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide orderSide,
            OrderType orderType,
            decimal quantity,
            decimal price,
            CancellationToken ct = default);

        Task<QueryResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        Task<QueryResult> CancelOrdersAsync(IEnumerable<BitunixCancelOrderRequest> orders, CancellationToken ct = default);
    }
}
