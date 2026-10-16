using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;
using System.Linq;

namespace Bitunix.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitunixCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Spot/Account", "https://openapi.bitunix.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", "123", "123", 0.1m, "123"), "Withdraw", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.CancelWithdrawalAsync("123"), "CancelWithdrawal");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync(TransferType.SpotToFutures, "123", 0.1m), "Transfer", nestedJsonProperty: "data");
            //await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalHistoryAsync(), "GetWithdrawalHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositsAsync(), "GetDeposits", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("123", "123"), "GetDepositAddress", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Spot/ExchangeData", "https://openapi.bitunix.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLastPriceAsync("ETHUSDT"), "GetLastPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneHour), "GetKlines", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("ETHUSDT", KlineInterval.OneHour), "GetKlineHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", nestedJsonProperty: "data",
                ignoreProperties: ["quoteFullNameI18n", "baseFullNameI18n"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeRatesAsync(), "GetExchangeRates", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets", nestedJsonProperty: "data");

        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitunixCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Spot/Trading", "https://openapi.bitunix.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", OrderSide.Sell, OrderType.Limit, 0.1m, 0.1m), "PlaceOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderHistoryAsync("ETHUSDT"), "GetOrderHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("123"), "GetOrder", nestedJsonProperty: "data",
                ignoreProperties: ["type"]);
        }

        private bool IsAuthenticated(IHttpResult result)
        {
            return result.RequestHeaders?.Any(x => x.Key == "sign") == true;
        }
    }
}
