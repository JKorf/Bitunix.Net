using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Objects.Models;
using Bitunix.Net.Objects.Options;

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
            var tester = new SocketSubscriptionValidator<BitunixSocketClient>(client, "Subscriptions/Spot", "XXX");
            //await tester.ValidateAsync<BitunixModel>((client, handler) => client.SpotApi.SubscribeToXXXUpdatesAsync(handler), "XXX");
        }

        [TestCase]
        public async Task ValidateConcurrentSpotSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitunixSocketClient(Options.Create(new BitunixSocketOptions
            {
                ApiCredentials = new BitunixCredentials("123", "456"),
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<BitunixSocketClient>(client, "Subscriptions/Spot", "XXX", "data");
            //await tester.ValidateConcurrentAsync<BitunixModel>(
            //    (client, handler) => client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.EightHour, handler),
            //    (client, handler) => client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.OneHour, handler),
            //    "Concurrent");
        }
    }
}
