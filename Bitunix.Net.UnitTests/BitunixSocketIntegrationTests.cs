using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Objects.Options;

namespace Bitunix.Net.UnitTests
{
    [NonParallelizable]
    internal class BitunixSocketIntegrationTests : SocketIntegrationTest<BitunixSocketClient>
    {
        public override bool Run { get; set; } = false;

        public BitunixSocketIntegrationTests()
        {
        }

        public override BitunixSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BitunixSocketClient(Options.Create(new BitunixSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new BitunixCredentials(key, sec) : null
            }), loggerFactory);
        }

        [TestCase]
        public async Task TestSubscriptions()
        {
            //await RunAndCheckUpdate<>((client, updateHandler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync(default, default, default, default, default, default, default, default), false, true);
            //await RunAndCheckUpdate<>((client, updateHandler) => client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
