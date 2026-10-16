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

            Spot = new OrderBookFactory<BitunixOrderBookOptions>(CreateSpot, Create);

        }

                 /// <inheritdoc />
        public IOrderBookFactory<BitunixOrderBookOptions> Futures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<BitunixOrderBookOptions> Spot { get; }


        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<BitunixOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(BitunixExchange.FormatSymbol);
            throw new NotImplementedException();

#warning TODO
            //if (symbol.TradingMode == TradingMode.Spot)
            //    return CreateSpot(symbolName, options);

            //return Create(symbolName, options);
        }

                 /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<BitunixOrderBookOptions>? options = null)
            => new BitunixFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IBitunixRestClient>(),
                                                          _serviceProvider.GetRequiredService<IBitunixSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<BitunixOrderBookOptions>? options = null)
            => new BitunixSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IBitunixRestClient>(),
                                                          _serviceProvider.GetRequiredService<IBitunixSocketClient>());


    }
}
