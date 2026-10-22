// 01-unified-quickstart.cs
//
// Demonstrates: public Spot data, authenticated balances, and a guarded Spot
// order workflow. Symbols use the compact Bitunix form, for example ETHUSDT.
//
// Setup: dotnet add package Bitunix.Net

using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;

// Public market-data calls do not require credentials.
using var publicClient = new BitunixRestClient();

var price = await publicClient.SpotApi.ExchangeData
    .GetLastPriceAsync("ETHUSDT");

if (!price.Success)
{
    Console.WriteLine($"Could not get ETHUSDT: {price.Error}");
    return;
}

Console.WriteLine($"ETHUSDT last price: {price.Data}");

var symbols = await publicClient.SpotApi.ExchangeData.GetSymbolsAsync();
if (symbols.Success)
{
    var ethUsdt = symbols.Data.FirstOrDefault(x => x.Symbol == "ETHUSDT");
    if (ethUsdt is not null)
    {
        Console.WriteLine(
            $"Minimum quantity: {ethUsdt.MinOrderQuantity}; " +
            $"minimum value: {ethUsdt.MinOrderValue}");
    }
}

// Private endpoints require an API key and secret. Keeping credentials outside
// source control also prevents an accidental live request from this example.
var apiKey = Environment.GetEnvironmentVariable("BITUNIX_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("BITUNIX_API_SECRET");

if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
{
    Console.WriteLine("Set BITUNIX_API_KEY and BITUNIX_API_SECRET to run private calls.");
    return;
}

using var privateClient = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials(apiKey, apiSecret);
});

var balances = await privateClient.SpotApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Balance request failed: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Where(x => x.Balance > 0))
    Console.WriteLine($"{balance.Asset}: total={balance.Balance}, locked={balance.BalanceLocked}");

// There is no test-order endpoint. Leave this false until symbol filters,
// balances, quantity, price, and live-order intent have all been validated.
var placeLiveOrder = false;
if (!placeLiveOrder)
{
    Console.WriteLine("Live Spot order skipped.");
    return;
}

var order = await privateClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success)
{
    Console.WriteLine($"Order failed: {order.Error}");
    return;
}

Console.WriteLine($"Placed Spot order {order.Data.OrderId}");

var orderInfo = await privateClient.SpotApi.Trading
    .GetOrderAsync(order.Data.OrderId);

Console.WriteLine(orderInfo.Success
    ? $"Order status: {orderInfo.Data.Status}"
    : $"Order lookup failed: {orderInfo.Error}");
