using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Bitunix.Net.Interfaces;
using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;

namespace Bitunix.Net.SymbolOrderBooks
{
    /// <summary>
    /// Bitunix order book factory
    /// </summary>
    public class BitunixOrderBookFactory : IBitunixOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitunixOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            Futures = new OrderBookFactory<BitunixOrderBookOptions>(CreateFutures, Create);
        }

        /// <inheritdoc />
        public IOrderBookFactory<BitunixOrderBookOptions> Futures { get; }


        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<BitunixOrderBookOptions>? options = null)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException("Spot API does not support order book", nameof(symbol));

            var symbolName = symbol.GetSymbol(BitunixExchange.FormatSymbol);
            return CreateFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<BitunixOrderBookOptions>? options = null)
            => new BitunixFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IBitunixSocketClient>());
    }
}
