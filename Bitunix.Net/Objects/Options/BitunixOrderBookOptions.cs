using CryptoExchange.Net.Objects.Options;
using System;

namespace Bitunix.Net.Objects.Options
{
    /// <summary>
    /// Options for the Bitunix SymbolOrderBook
    /// </summary>
    public class BitunixOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BitunixOrderBookOptions Default { get; set; } = new BitunixOrderBookOptions();

        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal BitunixOrderBookOptions Copy()
        {
            var result = Copy<BitunixOrderBookOptions>();
            result.Limit = Limit;
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
