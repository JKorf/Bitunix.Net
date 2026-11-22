using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Interfaces.Clients.FuturesApi;
using Bitunix.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Bitunix.Net.UnitTests
{
    [TestFixture]
    public class BitunixSharedApiRestIntegrationTest : SharedApiRestIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            await ExecuteAsync();
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBitunix();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBitunixSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedRest client)
        {
            if (client is IBitunixRestClientSpotSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");
            if (client is IBitunixRestClientFuturesSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");

            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class BitunixSharedApiSocketIntegrationTest : SharedApiSocketIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            var result = await ExecuteAsync();
            foreach (var item in result)
                Assert.Warn(item.Message);
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBitunix();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBitunixSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedSocket client)
        {
            if (client is IBitunixSocketClientSpotSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");
            if (client is IBitunixSocketClientFuturesSharedApi)
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");

            throw new NotImplementedException();
        }
    }
}
