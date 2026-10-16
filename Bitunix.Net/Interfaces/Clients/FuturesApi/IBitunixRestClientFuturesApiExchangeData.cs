using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;

namespace Bitunix.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Bitunix Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitunixRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
