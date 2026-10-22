using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Objects.Models;

namespace Bitunix.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSubscriptions()
        {
            var client = new BitunixSocketClient(opts =>
            {
                opts.ApiCredentials = new BitunixCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BitunixSocketClient>(client, "Subscriptions/Futures", "wss://fapi.bitunix.com/public/");
            await tester.ValidateAsync<BitunixTickerUpdate>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", "data");
            await tester.ValidateAsync<BitunixOrderBookUpdate>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 5, handler), "OrderBook", "data");
            await tester.ValidateAsync<BitunixKlineUpdate>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.FuturesSocketKlineInterval.OneHour, null, handler), "Kline", "data");
            await tester.ValidateAsync<BitunixMarkPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToMarkPriceUpdatesAsync("ETHUSDT", handler), "MarkPrice", "data");
            await tester.ValidateAsync<BitunixTradeUpdate[]>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", "data");
            await tester.ValidateAsync<BitunixAggTickerUpdate[]>((client, handler) => client.FuturesApi.SubscribeToAggregatedTickerUpdatesAsync("ETHUSDT", handler), "AggTicker", "data");
            await tester.ValidateAsync<BitunixBalanceUpdate>((client, handler) => client.FuturesApi.SubscribeToUserUpdatesAsync(handler), "Balance", "data");
            await tester.ValidateAsync<BitunixFuturesOrderUpdate>((client, handler) => client.FuturesApi.SubscribeToUserUpdatesAsync(null, handler), "Order", "data");
        }
    }
}
