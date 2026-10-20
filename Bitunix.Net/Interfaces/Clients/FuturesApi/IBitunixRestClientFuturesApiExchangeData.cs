using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitunixRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_tickers.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/tickers<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesTicker[]>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_depth.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/depth<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] The depth, 1, 5, 15 or 50. null for full book</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesOrderBook>> GetOrderBookAsync(
            string symbol,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get funding info for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_funding_rate_batch.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/funding_rate/batch<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFundingRate[]>> GetFundingRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_funding_rate_history.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/get_funding_rate_history<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFundingEntry[]>> GetFundingRateHistoryAsync(
            string symbol,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get funding info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_funding_rate.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/funding_rate<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_kline.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/kline<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="type">["<c>type</c>"] Kline type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesKline[]>> GetKlinesAsync(
            string symbol,
            FuturesKlineInterval interval,
            PriceType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get symbols and trading rules
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/market/get_trading_pairs.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/market/trading_pairs<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbol</c>"] Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesSymbol[]>> GetSymbolsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get position tiers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/position/get_position_tiers.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/position/get_position_tiers<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPositionTier[]>> GetPositionTiersAsync(string symbol, CancellationToken ct = default);

    }
}
