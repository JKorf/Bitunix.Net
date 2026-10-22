---
name: bitunix-net
description: Use Bitunix.Net when generating C#/.NET code for the Bitunix cryptocurrency exchange, including Spot and Futures REST operations, Futures WebSocket streams, Spot WebSocket order queries, account management, trading, local order books, or CryptoExchange.Net SharedApis.
---

# Bitunix.Net Skill

## Quick Decision

Use `Bitunix.Net` for Bitunix API access in C#/.NET. Do not write raw `HttpClient` or manual WebSocket code. The library handles authentication, request/response serialization, rate limiting integration, typed models, and socket connection management.

Use `BitunixRestClient` for REST calls and `BitunixSocketClient` for socket operations. The API is divided into Spot and Futures roots. Bitunix symbols are concatenated uppercase pairs such as `ETHUSDT`.

## Installation

```bash
dotnet add package Bitunix.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, and net10.0. Native AOT is supported on compatible targets.

## REST Client Setup

Public endpoints do not require credentials:

```csharp
using Bitunix.Net.Clients;

using var client = new BitunixRestClient();
var price = await client.SpotApi.ExchangeData.GetLastPriceAsync("ETHUSDT");
if (!price.Success) { Console.WriteLine(price.Error); return; }
Console.WriteLine(price.Data);
```

Private account and trading endpoints require `BitunixCredentials`:

```csharp
using Bitunix.Net;
using Bitunix.Net.Clients;

using var client = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials("API_KEY", "API_SECRET");
});
```

## Result Types

- Direct REST methods return `HttpResult<T>` or `HttpResult`.
- Futures subscriptions return `WebSocketResult<UpdateSubscription>`.
- Spot socket order operations return `QueryResult<T>` or `QueryResult`; these are request/response socket queries, not subscriptions.
- Shared API REST and subscription methods use the CryptoExchange.Net result contracts for their capability.

Always check `.Success` before reading `.Data` or passing a subscription to `UnsubscribeAsync`.

## API Surface

```text
BitunixRestClient
  .SpotApi
    .ExchangeData  latest price, depth, klines, symbols, rates, assets
    .Account       balances, withdrawals, transfers, deposits, addresses
    .Trading       Spot order placement, cancellation, queries, trades
    .SharedApi
  .FuturesApi
    .ExchangeData  tickers, depth, funding, klines, symbols, tiers
    .Account       balance, settings, position/margin mode, leverage
    .Trading       orders, positions, trades, TP/SL
    .SharedApi

BitunixSocketClient
  .SpotApi         place/cancel Spot order queries; no market streams
    .SharedApi
  .FuturesApi      public market and authenticated user subscriptions
    .SharedApi
```

There is no `GeneralApi`, `UsdtFuturesApi`, `CoinFuturesApi`, margin API, or options API. The derivatives root is named `FuturesApi`.

## Spot Order Pattern

```csharp
using Bitunix.Net.Enums;

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.01m,
    price: 2000m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
Console.WriteLine(order.Data.Id);
```

Spot orders support only the parameters exposed by the interface: symbol, side, type, quantity, and optional price. Do not add a time-in-force or client-order-id parameter to the direct Spot method.

## Futures Order Pattern

```csharp
using Bitunix.Net.Enums;

var leverage = await client.FuturesApi.Account.SetLeverageAsync(
    symbol: "ETHUSDT",
    marginAsset: "USDT",
    leverage: 5);

if (!leverage.Success) { Console.WriteLine(leverage.Error); return; }

var order = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.1m,
    price: 2000m,
    tradeSide: TradeSide.Open,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success) { Console.WriteLine(order.Error); return; }
Console.WriteLine(order.Data.Id);
```

`tradeSide` is required by Bitunix in hedge mode. Use `TradeSide.Open` or `TradeSide.Close` as appropriate. The Futures method also supports `positionId`, `clientOrderId`, `reduceOnly`, and inline take-profit/stop-loss parameters.

## Futures WebSocket Pattern

Public Futures streams do not need credentials:

```csharp
using Bitunix.Net.Clients;

using var socketClient = new BitunixSocketClient();
var sub = await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { Console.WriteLine(sub.Error); return; }

// Keep running until shutdown, then:
await socketClient.UnsubscribeAsync(sub.Data);
```

Available Futures streams are ticker, aggregated ticker, order book, kline, mark price/funding, trades, and authenticated user updates. `SubscribeToUserUpdatesAsync` can receive balance, order, position, and TP/SL callbacks; credentials are configured on the socket client, and no listen key is used.

```csharp
using var privateSocket = new BitunixSocketClient(options =>
{
    options.ApiCredentials = new BitunixCredentials("API_KEY", "API_SECRET");
});

var userSub = await privateSocket.FuturesApi.SubscribeToUserUpdatesAsync(
    onBalanceMessage: update => Console.WriteLine(update.Data.Available),
    onOrderMessage: update => Console.WriteLine(update.Data.OrderId));
```

Spot sockets do not expose market-data or user-data subscriptions in the current interface. They expose authenticated `PlaceOrderAsync`, `CancelOrderAsync`, and `CancelOrdersAsync` query methods.

## SharedApis

Use an API root's `.SharedApi` when transport and trading mode are known:

```csharp
using Bitunix.Net.Clients;
using CryptoExchange.Net.SharedApis;

using var client = new BitunixRestClient();
IGetTickerRest tickerClient = client.FuturesApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT");

var ticker = await tickerClient.GetTickerAsync(new GetTickerRequest(symbol));
if (!ticker.Success) { Console.WriteLine(ticker.Error); return; }
Console.WriteLine(ticker.Data.LastPrice);
```

Use `IBitunixSharedApiClient` for runtime discovery and capability lookup. It groups `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket`.

Important capability boundaries:

- Spot REST supports assets, balances, deposits, deposit addresses, klines, order books, Spot symbols, withdrawals, and Spot order management.
- Spot REST does not currently implement `IGetTickerRest`; call direct `SpotApi.ExchangeData.GetLastPriceAsync` for a Spot price.
- Spot socket SharedApi supports placing and canceling Spot orders, not subscriptions.
- Futures REST supports market data, funding, leverage, position mode, orders, positions, trades, and TP/SL.
- Futures socket supports public market and private balance/order/position subscriptions.

## Dependency Injection

```csharp
using Bitunix.Net;
using Microsoft.Extensions.DependencyInjection;

services.AddBitunix(options =>
{
    options.ApiCredentials = new BitunixCredentials("API_KEY", "API_SECRET");
});
```

Inject `IBitunixRestClient`, `IBitunixSocketClient`, `IBitunixSharedApiClient`, `IBitunixOrderBookFactory`, or `IBitunixUserClientProvider` as needed.

## Environments

Only `BitunixEnvironment.Live` is built in. A custom environment requires all four addresses:

```csharp
var custom = BitunixEnvironment.CreateCustom(
    "custom",
    "https://spot-rest.example",
    "wss://spot-socket.example",
    "https://futures-rest.example",
    "wss://futures-socket.example");
```

Assign it with `options.Environment = custom`. Do not invent a Bitunix testnet environment.

## Hard Rules

- Do not generate raw HTTP requests, signing code, or raw WebSocket protocol code.
- Use `BitunixCredentials`, not generic `ApiCredentials`, in examples.
- Check `.Success` before `.Data`.
- Do not use `.Result` or `.Wait()`.
- Reuse clients or use dependency injection; do not create one per request.
- Unsubscribe successful Futures subscriptions on shutdown.
- Do not call Spot socket operations subscriptions; they return `QueryResult`.
- Do not invent Spot socket market streams or a Spot shared ticker capability.
- Do not invent a listen-key flow; Futures private sockets authenticate with configured credentials.
- Do not add unsupported parameters to direct Spot orders.
- Inspect `Bitunix.Net/Interfaces/Clients/**` when exact signatures matter.

## Reference

- Client guide: https://cryptoexchange.jkorf.dev/docs/exchange-clients?library=Bitunix.Net
- API reference: https://cryptoexchange.jkorf.dev/docs/exchange-clients/reference?library=Bitunix.Net
- LLM context: `llms.txt` and `llms-full.txt`
- Source of truth: `Bitunix.Net/Interfaces/Clients/**`
- Repository: https://github.com/JKorf/Bitunix.Net
