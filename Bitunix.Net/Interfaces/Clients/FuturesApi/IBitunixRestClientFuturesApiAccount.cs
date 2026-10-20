using Bitunix.Net.Enums;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitunixRestClientFuturesApiAccount
    {
        /// <summary>
        /// Adjust margin for an isolated position.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/adjust_position_margin.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/account/adjust_position_margin<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="asset">["<c>marginCoin</c>"] Margin asset</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="side">["<c>side</c>"] Position side</param>
        /// <param name="id">["<c>positionId</c>"] The position id</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> AdjustMarginAsync(
            string symbol,
            string asset,
            decimal quantity,
            PositionSide? side = null,
            string? id = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/get_single_account.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/account<br />
        /// </para>
        /// </summary>
        /// <param name="marginAsset">["<c>marginCoin</c>"] The margin asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixFuturesBalance>> GetBalancesAsync(string marginAsset, CancellationToken ct = default);

        /// <summary>
        /// Get trading settings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/get_trading_settings.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/account/trading_settings<br />
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixTradingSetting[]>> GetTradingSettingsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/get_position_mode.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/account/position_mode<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPositionMode>> GetPositionModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage and margin mode settings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/get_leverage_and_margin_mode.html" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/account/get_leverage_margin_mode<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="marginAsset">["<c>marginCoin</c>"] The margin asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixLeverageMargin>> GetLeverageMarginModeAsync(
            string symbol,
            string marginAsset,
            CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/change_position_mode.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/account/change_position_mode<br />
        /// </para>
        /// </summary>
        /// <param name="positionMode">["<c>positionMode</c>"] Position mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

        /// <summary>
        /// Set margin mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/change_margin_mode.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/account/change_margin_mode<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="asset">["<c>marginCoin</c>"] The margin asset, for example `USDT`</param>
        /// <param name="mode">["<c>marginMode</c>"] Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixMarginMode>> SetMarginModeAsync(
            string symbol,
            string asset,
            MarginMode mode,
            CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.bitunix.com/api-docs/futures/account/change_leverage.html" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/account/change_leverage<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="marginAsset">["<c>marginCoin</c>"] The margin asset, for example `USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] New leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitunixLeverage>> SetLeverageAsync(
            string symbol,
            string marginAsset,
            int leverage,
            CancellationToken ct = default);

    }
}
