// 02-derivatives.cs
//
// Demonstrates: public Futures data, balances, positions, leverage, and a
// guarded Futures order. Leverage and order placement are live mutations.
//
// Setup: dotnet add package Bitunix.Net

using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;

using var publicClient = new BitunixRestClient();

var tickers = await publicClient.FuturesApi.ExchangeData
    .GetTickersAsync(new[] { "ETHUSDT" });

if (!tickers.Success)
{
    Console.WriteLine($"Ticker request failed: {tickers.Error}");
    return;
}

var ticker = tickers.Data.SingleOrDefault();
if (ticker is not null)
    Console.WriteLine($"ETHUSDT last={ticker.LastPrice}, mark={ticker.MarkPrice}");

var apiKey = Environment.GetEnvironmentVariable("BITUNIX_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("BITUNIX_API_SECRET");

if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
{
    Console.WriteLine("Set BITUNIX_API_KEY and BITUNIX_API_SECRET to run private Futures calls.");
    return;
}

using var client = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials(apiKey, apiSecret);
});

var balance = await client.FuturesApi.Account.GetBalancesAsync("USDT");
if (!balance.Success)
{
    Console.WriteLine($"Futures balance failed: {balance.Error}");
    return;
}

Console.WriteLine(
    $"USDT available={balance.Data.Available}, frozen={balance.Data.Frozen}");

var positions = await client.FuturesApi.Trading
    .GetPositionsAsync(symbol: "ETHUSDT");

if (!positions.Success)
{
    Console.WriteLine($"Position request failed: {positions.Error}");
    return;
}

foreach (var position in positions.Data)
{
    Console.WriteLine(
        $"{position.PositionId}: {position.Side} {position.Quantity} " +
        $"at {position.AverageOpenPrice}; unrealized={position.UnrealizedPnl}");
}

// Keep mutations disabled until the account mode and intended exposure have
// been confirmed. In hedge mode, tradeSide is required by Bitunix.
var changeLeverageAndPlaceLiveOrder = false;
if (!changeLeverageAndPlaceLiveOrder)
{
    Console.WriteLine("Leverage change and live Futures order skipped.");
    return;
}

var leverage = await client.FuturesApi.Account.SetLeverageAsync(
    symbol: "ETHUSDT",
    marginAsset: "USDT",
    leverage: 5);

if (!leverage.Success)
{
    Console.WriteLine($"Leverage change failed: {leverage.Error}");
    return;
}

var order = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.01m,
    price: 1000m,
    tradeSide: TradeSide.Open,
    timeInForce: TimeInForce.GoodTillCanceled);

Console.WriteLine(order.Success
    ? $"Placed Futures order {order.Data.OrderId}"
    : $"Futures order failed: {order.Error}");
