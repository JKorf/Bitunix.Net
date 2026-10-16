using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Objects.Options;

namespace Bitunix.Net.UnitTests
{
    [NonParallelizable]
    public class BitunixRestIntegrationTests : RestIntegrationTest<BitunixRestClient>
    {
        public override bool Run { get; set; } = false;

        public override BitunixRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BitunixRestClient(null, loggerFactory, Options.Create(new BitunixRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new BitunixCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

#warning Implement error response
            //var result = await CreateClient().SpotApi.ExchangeData.GetTickerAsync("TSTTST", default);

            //Assert.That(result.Success, Is.False);
            //Assert.That(result.Error.Code, Is.EqualTo(-1121));
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            //await RunAndCheckResult(client => client.SpotApi.ExchangeData.PingAsync(CancellationToken.None), false);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }
    }
}
