using Bitunix.Net.Clients.MessageHandlers;
using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Options;
using Bitunix.Net.Objects.Sockets;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Bitunix Spot websocket Api
    /// </summary>
    internal partial class BitunixSocketClientSpotApi : SocketApiClient<BitunixEnvironment, BitunixAuthenticationProvider, BitunixCredentials>, IBitunixSocketClientSpotApi
    {
        #region fields
        private readonly BitunixSocketClientSpotSharedApi _sharedApi;

        protected override ErrorMapping ErrorMapping => BitunixErrors.SpotErrors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal BitunixSocketClientSpotApi(ILoggerFactory? loggerFactory, BitunixSocketOptions options) :
            base(loggerFactory, BitunixExchange.Metadata.Id, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            _sharedApi = new BitunixSocketClientSpotSharedApi(this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);
        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new BitunixSocketSpotMessageHandler();

        /// <inheritdoc />
        public IBitunixSocketClientSpotSharedApi SharedApi => _sharedApi;

        /// <inheritdoc />
        protected override BitunixAuthenticationProvider CreateAuthenticationProvider(BitunixCredentials credentials)
            => new BitunixAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<QueryResult<BitunixOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide orderSide,
            OrderType orderType,
            decimal quantity,
            decimal price,
            CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return QueryResult.Fail<BitunixOrderResult>(Exchange, new NoApiCredentialsError());

            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("volume", quantity);
            parameters.Add("price", price);
            var request = new BitunixSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "order.place_order",
                Params = parameters
            };

            AuthenticationProvider.AddAuth(this, request);
            var query = new BitunixQuery<BitunixOrderResult>(this, request, true);
            var result = await QueryAsync(BaseAddress.AppendPath("/ws-api/v1"), query, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (!result.Data.Success)
            {
                return QueryResult.Fail<BitunixOrderResult>(result,
                    new ServerError(result.Data.PlaceCode, GetErrorInfo(result.Data.PlaceCode, result.Data.PlaceMessage)));
            }

            return result;
        }


        #region Cancel Order

        /// <inheritdoc />
        public Task<QueryResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default)
            => CancelOrdersAsync([new BitunixCancelOrderRequest { Symbol = symbol, OrderId = orderId }], ct);

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<QueryResult> CancelOrdersAsync(IEnumerable<BitunixCancelOrderRequest> orders, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return QueryResult.Fail<BitunixOrderResult>(Exchange, new NoApiCredentialsError());

            var parameters = new Parameters(BitunixExchange._parameterSerializationSettings);
            parameters.Add("orderIdList", orders.ToArray());
            var request = new BitunixSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "order.cancel",
                Params = parameters
            };
            AuthenticationProvider.AddAuth(this, request);
            var query = new BitunixQuery<BitunixOrderResult>(this, request, true);
            var result = await QueryAsync(BaseAddress.AppendPath("/ws-api/v1"), query, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BitunixExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
