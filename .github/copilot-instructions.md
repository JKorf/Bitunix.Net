# Copilot Instructions for Bitunix.Net

This repository is Bitunix.Net, a strongly typed C#/.NET client for the Bitunix Spot and Futures REST and WebSocket APIs. It is built on CryptoExchange.Net.

## Use the Library

Generate `BitunixRestClient` or `BitunixSocketClient` code. Do not generate raw `HttpClient`, manual signatures, or raw WebSocket protocol code.

```csharp
using Bitunix.Net;
using Bitunix.Net.Clients;

var client = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials("API_KEY", "API_SECRET");
});
```

Public REST calls and public Futures streams do not require credentials.

## Results

REST methods return `HttpResult<T>` or `HttpResult`. Futures subscription methods return `WebSocketResult<UpdateSubscription>`. Spot socket order methods return `QueryResult<T>` or `QueryResult`. Check `.Success` before reading `.Data`.

## API Structure

- `client.SpotApi.ExchangeData`: latest price, depth, klines, symbols, rates, assets
- `client.SpotApi.Account`: balances, withdrawals, transfers, deposits, addresses
- `client.SpotApi.Trading`: Spot orders and user trades
- `client.FuturesApi.ExchangeData`: tickers, depth, funding, klines, symbols, tiers
- `client.FuturesApi.Account`: balance, trading settings, modes, leverage, margin
- `client.FuturesApi.Trading`: orders, positions, trades, TP/SL
- `socketClient.SpotApi`: authenticated Spot order placement/cancellation queries
- `socketClient.FuturesApi`: Futures ticker, aggregated ticker, depth, kline, mark price, trades, and user updates

There is no `UsdtFuturesApi`, `GeneralApi`, Coin Futures, margin, or options root.

## Orders

```csharp
using Bitunix.Net.Enums;

var spot = await client.SpotApi.Trading.PlaceOrderAsync(
    "ETHUSDT", OrderSide.Buy, OrderType.Limit, 0.01m, price: 2000m);

var futures = await client.FuturesApi.Trading.PlaceOrderAsync(
    "ETHUSDT", OrderSide.Buy, OrderType.Limit, 0.1m,
    price: 2000m,
    tradeSide: TradeSide.Open,
    timeInForce: TimeInForce.GoodTillCanceled);
```

Do not add time-in-force or client-order-id arguments to direct Spot order placement. Futures `tradeSide` is required in hedge mode; use `TradeSide.Open` or `TradeSide.Close`.

## Futures WebSocket

```csharp
var socketClient = new BitunixSocketClient();
var sub = await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT", update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { Console.WriteLine(sub.Error); return; }
await socketClient.UnsubscribeAsync(sub.Data);
```

Private Futures updates authenticate from `BitunixCredentials` on the socket options. Do not generate a listen-key flow. Spot socket methods are query operations and do not return `UpdateSubscription`.

## Shared API

Each root has `.SharedApi`. `IBitunixSharedApiClient` groups `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket` for runtime discovery.

Respect strict capabilities. In particular, Spot REST currently has no shared ticker capability: use direct `SpotApi.ExchangeData.GetLastPriceAsync` for a Spot price. Spot socket SharedApi supports placing/canceling orders, not subscribing to data. Futures has shared ticker and public/private stream capabilities.

## Generation Rules

- Use `BitunixCredentials`, never generic `ApiCredentials`, in examples.
- Use symbol format `ETHUSDT`.
- Check `.Success` before `.Data`.
- Keep operations async; never use `.Result` or `.Wait()`.
- Reuse clients or configure `services.AddBitunix(...)`.
- Unsubscribe successful Futures subscriptions on shutdown.
- Only `BitunixEnvironment.Live` is predefined; use `CreateCustom` only when custom endpoints are required.
- Do not invent endpoints, overloads, callbacks, or SharedApis capabilities. Inspect `Bitunix.Net/Interfaces/Clients/**`.

See `AGENTS.md` for primary patterns and `llms-full.txt` for the detailed endpoint and SharedApis map.
