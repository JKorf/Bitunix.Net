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


        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitunixCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Futures/Account", "https://fapi.bitunix.com", IsAuthenticated); 
            await tester.ValidateAsync(client => client.FuturesApi.Account.AdjustMarginAsync("ETHUSDT", "123", 0.1m), "AdjustMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetBalancesAsync("123"), "GetBalances", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTradingSettingsAsync(), "GetTradingSettings", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetLeverageMarginModeAsync("ETHUSDT", "123"), "GetLeverageMarginMode", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetPositionModeAsync(PositionMode.HedgeMode), "SetPositionMode", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetMarginModeAsync("ETHUSDT", "123", MarginMode.Isolated), "SetMarginMode", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetLeverageAsync("ETHUSDT", "123", 123), "SetLeverage", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Futures/ExchangeData", "https://fapi.bitunix.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickersAsync(), "GetTickers", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRatesAsync(), "GetFundingRates", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETHUSDT"), "GetFundingRateHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateAsync("ETHUSDT"), "GetFundingRate", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", FuturesKlineInterval.OneMinute), "GetKlines", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetPositionTiersAsync("ETHUSDT"), "GetPositionTiers", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new BitunixRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitunixCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitunixRestClient>(client, "Endpoints/Futures/Trading", "https://fapi.bitunix.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceOrderAsync("ETHUSDT", OrderSide.Buy, OrderType.Limit, 0.1m), "PlaceOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.EditOrderAsync("123", null, 0.1m), "EditOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelOrdersAsync("ETHUSDT", []), "CancelOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllOrdersAsync(), "CancelAllOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.ClosePositionAsync("123"), "ClosePosition", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderHistoryAsync(), "GetOrderHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderAsync("123"), "GetOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetUserTradesAsync(), "GetUserTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionsAsync(), "GetPositions", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionHistoryAsync(), "GetPositionHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceTpSlOrderAsync("ETHUSDT", "123"), "PlaceTpSlOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlacePositionTpSlOrderAsync("ETHUSDT", "123"), "PlacePositionTpSlOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelTpSlOrderAsync("ETHUSDT", "123"), "CancelTpSlOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenTpSlOrdersAsync("ETHUSDT", "123", 123), "GetOpenTpSlOrders", nestedJsonProperty: "data");
            //await tester.ValidateAsync(client => client.FuturesApi.Trading.GetTpSlOrderHistoryAsync(), "GetTpSlOrderHistory", nestedJsonProperty: "data");
        }

        private bool IsAuthenticated(IHttpResult result)
        {
            return result.RequestHeaders?.Any(x => x.Key == "sign") == true;
        }
    }
}
