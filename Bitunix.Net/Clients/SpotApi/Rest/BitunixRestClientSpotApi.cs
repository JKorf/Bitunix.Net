using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using Bitunix.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Bitunix.Net.Clients.MessageHandlers;
using Bitunix.Net.Objects.Models;

namespace Bitunix.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitunixRestClientSpotApi" />
    internal partial class BitunixRestClientSpotApi : RestApiClient<BitunixEnvironment, BitunixAuthenticationProvider, BitunixCredentials>, IBitunixRestClientSpotApi
    {
        #region fields 
        private readonly BitunixRestClientSpotSharedApi _sharedApi;

        protected override ErrorMapping ErrorMapping => BitunixErrors.SpotErrors;

        /// <inheritdoc />
        protected override IRestMessageHandler MessageHandler { get; } = new BitunixRestSpotMessageHandler(BitunixErrors.SpotErrors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitunixRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBitunixRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitunixRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Bitunix";

        #endregion

        #region constructor/destructor
        internal BitunixRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, BitunixRestOptions options)
            : base(loggerFactory, BitunixExchange.Metadata.Id, httpClient, options.Environment.SpotRestClientAddress, options, options.SpotOptions)
        {
            RequestBodyEmptyContent = "";

            Account = new BitunixRestClientSpotApiAccount(this);
            ExchangeData = new BitunixRestClientSpotApiExchangeData(_logger, this);
            Trading = new BitunixRestClientSpotApiTrading(_logger, this);

            _sharedApi = new BitunixRestClientSpotSharedApi(this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);


        /// <inheritdoc />
        protected override BitunixAuthenticationProvider CreateAuthenticationProvider(BitunixCredentials credentials)
            => new BitunixAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<Unit>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitunixResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
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

        /// <inheritdoc />
        public IBitunixRestClientSpotSharedApi SharedApi => _sharedApi;
    }
}
