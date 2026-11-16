# Bitunix.Net Usage Patterns

Use this reference for practical snippets after selecting the correct surface in [API surfaces](api-surfaces.md). Snippets using `client` assume a reused `BitunixRestClient`, configured with credentials for private calls. Private examples use placeholders and change live account state where they submit orders or settings.

## Imports

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;
using Bitunix.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public Spot Price

```csharp
using var client = new BitunixRestClient();
var result = await client.SpotApi.ExchangeData
    .GetLastPriceAsync("ETHUSDT");

if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data);
```

## Public Futures Data

```csharp
var tickers = await client.FuturesApi.ExchangeData
    .GetTickersAsync(new[] { "ETHUSDT" });

var funding = await client.FuturesApi.ExchangeData
    .GetFundingRateAsync("ETHUSDT");
```

Check each result independently before accessing data.

## Credentials And Balances

```csharp
using var client = new BitunixRestClient(options =>
{
    options.ApiCredentials =
        new BitunixCredentials("API_KEY", "API_SECRET");
});

var spotBalances = await client.SpotApi.Account.GetBalancesAsync();
if (!spotBalances.Success)
{
    Console.WriteLine(spotBalances.Error);
    return;
}
foreach (var balance in spotBalances.Data)
    Console.WriteLine($"{balance.Asset}: {balance.Balance}");
var futuresBalance = await client.FuturesApi.Account.GetBalancesAsync("USDT");
if (!futuresBalance.Success)
{
    Console.WriteLine(futuresBalance.Error);
    return;
}
Console.WriteLine(futuresBalance.Data.Available);
```

Bitunix Futures balance lookup requires the margin asset.

## Spot Limit Order

```csharp
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.01m,
    price: 2000m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

var cancel = await client.SpotApi.Trading.CancelOrderAsync(
    "ETHUSDT", order.Data.OrderId);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

There is no test-order endpoint. A valid placement request can create a live order.

## Futures Settings And Order

```csharp
var mode = await client.FuturesApi.Account
    .SetMarginModeAsync("ETHUSDT", "USDT", MarginMode.Isolated);

if (!mode.Success)
{
    Console.WriteLine(mode.Error);
    return;
}

var leverage = await client.FuturesApi.Account
    .SetLeverageAsync("ETHUSDT", "USDT", 5);

if (!leverage.Success)
{
    Console.WriteLine(leverage.Error);
    return;
}

var order = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.1m,
    price: 2000m,
    tradeSide: TradeSide.Open,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
    Console.WriteLine(order.Error);
```

Setting margin mode/leverage and placing the order are separate live mutations. In one-way mode, omit `tradeSide` unless the endpoint semantics require it and use `reduceOnly` deliberately when reducing exposure.

## Futures Ticker Subscription

```csharp
using var socket = new BitunixSocketClient();
var sub = await socket.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success)
{
    Console.WriteLine(sub.Error);
    return;
}

try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socket.UnsubscribeAsync(sub.Data);
}
```

Production services should tie shutdown to a cancellation token.

## Futures User Updates

```csharp
using var socket = new BitunixSocketClient(options =>
{
    options.ApiCredentials =
        new BitunixCredentials("API_KEY", "API_SECRET");
});

var sub = await socket.FuturesApi.SubscribeToUserUpdatesAsync(
    onBalanceMessage: update => Console.WriteLine("Balance update"),
    onOrderMessage: update => Console.WriteLine("Order update"),
    onPositionMessage: update => Console.WriteLine("Position update"),
    onTpSlMessage: update => Console.WriteLine("TP/SL update"));

if (!sub.Success)
{
    Console.WriteLine(sub.Error);
    return;
}

try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socket.UnsubscribeAsync(sub.Data);
}
```

No REST listen key is created or renewed.

## Spot Socket Query

```csharp
using var socket = new BitunixSocketClient(options =>
{
    options.ApiCredentials =
        new BitunixCredentials("API_KEY", "API_SECRET");
});

var order = await socket.SpotApi.PlaceOrderAsync(
    "ETHUSDT",
    OrderSide.Buy,
    OrderType.Limit,
    0.01m,
    price: 2000m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.OrderId);
```

This returns `QueryResult<BitunixOrderResult>`, not a subscription.

## Shared Futures Ticker

```csharp
using var client = new BitunixRestClient();
IGetTickerRest ticker = client.FuturesApi.SharedApi;

var request = new GetTickerRequest(
    new SharedSymbol(
        TradingMode.PerpetualLinear,
        "ETH",
        "USDT"));

var result = await ticker.GetTickerAsync(request);
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

For Spot prices, call native `GetLastPriceAsync`; Spot SharedApi has no ticker capability.

## Dependency Injection

```csharp
var services = new ServiceCollection();
services.AddBitunix(options =>
{
    options.ApiCredentials = new BitunixCredentials("API_KEY", "API_SECRET");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<IBitunixRestClient>();
var shared = provider.GetRequiredService<IBitunixSharedApiClient>();
```

## Error Handling

```csharp
var result = await client.SpotApi.ExchangeData
    .GetLastPriceAsync("ETHUSDT");

if (!result.Success)
{
    if (result.Error?.IsTransient == true)
    {
        // Apply bounded backoff and cancellation.
    }

    Console.WriteLine(
        $"{result.Error?.Code}: {result.Error?.Message}");
    return;
}
```

API failures are represented in result errors. Do not rely on exceptions for normal API rejections.


## Complete examples (checkout-only)

The documentation test compiles these programs without executing exchange operations. Running them contacts Bitunix; review their account operations before enabling live actions.

- [01-unified-quickstart](../../../../Examples/ai-friendly/01-unified-quickstart.cs)
- [02-derivatives](../../../../Examples/ai-friendly/02-derivatives.cs)
- [03-websocket](../../../../Examples/ai-friendly/03-websocket.cs)
- [04-multi-exchange](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [05-error-handling](../../../../Examples/ai-friendly/05-error-handling.cs)
