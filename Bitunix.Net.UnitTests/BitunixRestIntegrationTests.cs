using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Objects.Options;
using System.Threading;

namespace Bitunix.Net.UnitTests
{
    [NonParallelizable]
    public class BitunixRestIntegrationTests : RestIntegrationTest<BitunixRestClient>
    {
        public override bool Run { get; set; } = true;

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

            var result = await CreateClient().SpotApi.ExchangeData.GetLastPriceAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(2));
        }

        [Test]
        public async Task TestSpotAccountData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.Account.GetBalancesAsync(CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalHistoryAsync(default, default, "ETH", default, default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositsAsync(default, "USDT", default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositAddressAsync("USDT", "Tron (TRC-20)", CancellationToken.None), true, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetExchangeRatesAsync(CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("ETHUSDT", Enums.KlineInterval.OneDay, null, null, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLastPriceAsync("ETHUSDT", CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(CancellationToken.None), false, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTradingData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOrderHistoryAsync("ETHUSDT", default, default, default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT", CancellationToken.None), true, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesAccountData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.FuturesApi.Account.GetBalancesAsync("USDT", CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Account.GetTradingSettingsAsync(default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Account.GetPositionModeAsync(CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Account.GetLeverageMarginModeAsync("ETHUSDT", "USDT", CancellationToken.None), true, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetTickersAsync(default, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetFundingRatesAsync(CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETHUSDT", default, default, default, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetFundingRateAsync("ETHUSDT", CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", default, default, default, default, default, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetSymbolsAsync(default, CancellationToken.None), false, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetPositionTiersAsync("ETHUSDT", CancellationToken.None), false, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesTradingData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetOrderHistoryAsync(default, default, default, default, default, default, default, default, default, default, CancellationToken.None), true, true, "data", ignoreProperties: ["price"]);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetPositionsAsync(default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetPositionHistoryAsync(default, default, default, default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetOpenTpSlOrdersAsync(default, default, default, default, CancellationToken.None), true, true, "data");
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetTpSlOrderHistoryAsync(default, default, default, default, default, CancellationToken.None), true, true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

    }
}
