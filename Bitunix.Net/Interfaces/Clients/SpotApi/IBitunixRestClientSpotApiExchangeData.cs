using System;
using System.Threading;
using System.Threading.Tasks;
using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitunix.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitunix Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitunixRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get the latest trade price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#1-get-latest-price" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/market/last_price<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<decimal>> GetLastPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the order book for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#2-get-depth-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/market/depth<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#3-get-k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/market/kline<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixKline[]>> GetKlinesAsync(
            string symbol,
            KlineInterval interval,
            CancellationToken ct = default);

        /// <summary>
        /// Get kline history data for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#4-get-k-line-history-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/market/kline/history<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="endTime">["<c>endTime</c>"] End time filter</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixKline[]>> GetKlineHistoryAsync(
            string symbol,
            KlineInterval interval,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#5-query-trading-pair-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/common/coin_pair/list<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#6-query-rate-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/common/rate/list<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixExchangeRate[]>> GetExchangeRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get asset and network info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/spots/en_us/public/#7-query-token-data" /><br />
        /// Endpoint:<br />
        /// GET /api/spot/v1/common/coin/coin_network/list<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixAsset[]>> GetAssetsAsync(CancellationToken ct = default);

    }
}
