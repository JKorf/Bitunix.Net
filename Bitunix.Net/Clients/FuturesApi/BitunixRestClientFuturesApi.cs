using Bitunix.Net.Clients.MessageHandlers;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IBitunixRestClientFuturesApi" />
    internal partial class BitunixRestClientFuturesApi : RestApiClient<BitunixEnvironment, BitunixAuthenticationProvider, BitunixCredentials>, IBitunixRestClientFuturesApi
    {
        #region fields 
        protected override ErrorMapping ErrorMapping => BitunixErrors.Errors;

        /// <inheritdoc />
        protected override IRestMessageHandler MessageHandler { get; } = new BitunixRestFuturesMessageHandler(BitunixErrors.Errors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitunixRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBitunixRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitunixRestClientFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Bitunix";
        #endregion

        #region constructor/destructor
        internal BitunixRestClientFuturesApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, BitunixRestOptions options)
            : base(loggerFactory, BitunixExchange.Metadata.Id, httpClient, options.Environment.FuturesRestClientAddress, options, options.FuturesOptions)
        {
            Account = new BitunixRestClientFuturesApiAccount(this);
            ExchangeData = new BitunixRestClientFuturesApiExchangeData(_logger, this);
            Trading = new BitunixRestClientFuturesApiTrading(_logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);


        /// <inheritdoc />
        protected override BitunixAuthenticationProvider CreateAuthenticationProvider(BitunixCredentials credentials)
            => new BitunixAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitunixFuturesResponse>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitunixFuturesResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }


        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => throw new NotImplementedException();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => BitunixExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

    }
}
