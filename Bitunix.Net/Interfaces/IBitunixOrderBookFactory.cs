using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;
using Bitunix.Net.Objects.Options;

namespace Bitunix.Net.Interfaces
{
    /// <summary>
    /// Bitunix local order book factory
    /// </summary>
    public interface IBitunixOrderBookFactory
    {
                /// <summary>
        /// Futures order book factory methods
        /// </summary>
        IOrderBookFactory<BitunixOrderBookOptions> Futures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<BitunixOrderBookOptions> Spot { get; }


        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<BitunixOrderBookOptions>? options = null);

                /// <summary>
        /// Create a new Futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<BitunixOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<BitunixOrderBookOptions>? options = null);

    }
}