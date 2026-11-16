# Bitunix.Net API Surfaces

Use this reference when choosing native client roots, endpoint groups, result types, socket operations, SharedApis capabilities, environments, or symbol formats.

## Package And Client Types

- NuGet package: `Bitunix.Net`
- REST client/interface: `BitunixRestClient` / `IBitunixRestClient`
- Socket client/interface: `BitunixSocketClient` / `IBitunixSocketClient`
- Credentials: `BitunixCredentials`
- Shared aggregate: `IBitunixSharedApiClient`
- DI extension: `services.AddBitunix(...)`
- Local order book factory: `IBitunixOrderBookFactory`
- Tracker factory: `IBitunixTrackerFactory`
- User client provider: `IBitunixUserClientProvider`

## Client Roots

REST:

- `client.SpotApi`
- `client.FuturesApi`

Sockets:

- `socketClient.SpotApi`
- `socketClient.FuturesApi`

Do not use `GeneralApi`, `UsdtFuturesApi`, `UsdFuturesApi`, `CoinFuturesApi`, or `UnifiedApi`.

## Spot REST

### Market data

| Intent | Method |
| --- | --- |
| Get Last Price | `client.SpotApi.ExchangeData.GetLastPriceAsync(...)` |
| Get Order Book | `client.SpotApi.ExchangeData.GetOrderBookAsync(...)` |
| Get Klines | `client.SpotApi.ExchangeData.GetKlinesAsync(...)` |
| Get Kline History | `client.SpotApi.ExchangeData.GetKlineHistoryAsync(...)` |
| Get Symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync(...)` |
| Get Exchange Rates | `client.SpotApi.ExchangeData.GetExchangeRatesAsync(...)` |
| Get Assets | `client.SpotApi.ExchangeData.GetAssetsAsync(...)` |

### Account

| Intent | Method |
| --- | --- |
| Get Balances | `client.SpotApi.Account.GetBalancesAsync(...)` |
| Withdraw | `client.SpotApi.Account.WithdrawAsync(...)` |
| Cancel Withdrawal | `client.SpotApi.Account.CancelWithdrawalAsync(...)` |
| Transfer | `client.SpotApi.Account.TransferAsync(...)` |
| Get Withdrawal History | `client.SpotApi.Account.GetWithdrawalHistoryAsync(...)` |
| Get Deposits | `client.SpotApi.Account.GetDepositsAsync(...)` |
| Get Deposit Address | `client.SpotApi.Account.GetDepositAddressAsync(...)` |

### Trading

| Intent | Method |
| --- | --- |
| Place Order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Cancel Order | `client.SpotApi.Trading.CancelOrderAsync(...)` |
| Cancel Orders | `client.SpotApi.Trading.CancelOrdersAsync(...)` |
| Get Order History | `client.SpotApi.Trading.GetOrderHistoryAsync(...)` |
| Get Open Orders | `client.SpotApi.Trading.GetOpenOrdersAsync(...)` |
| Get Order | `client.SpotApi.Trading.GetOrderAsync(...)` |
| Get User Trades | `client.SpotApi.Trading.GetUserTradesAsync(...)` |

Direct Spot placement has no `timeInForce`, `clientOrderId`, or quote-quantity parameter.

## Futures REST

### Market data

| Intent | Method |
| --- | --- |
| Get Tickers | `client.FuturesApi.ExchangeData.GetTickersAsync(...)` |
| Get Order Book | `client.FuturesApi.ExchangeData.GetOrderBookAsync(...)` |
| Get Funding Rates | `client.FuturesApi.ExchangeData.GetFundingRatesAsync(...)` |
| Get Funding Rate History | `client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync(...)` |
| Get Funding Rate | `client.FuturesApi.ExchangeData.GetFundingRateAsync(...)` |
| Get Klines | `client.FuturesApi.ExchangeData.GetKlinesAsync(...)` |
| Get Symbols | `client.FuturesApi.ExchangeData.GetSymbolsAsync(...)` |
| Get Position Tiers | `client.FuturesApi.ExchangeData.GetPositionTiersAsync(...)` |

Direct order-book limits are 1, 5, 15, or 50; null requests the full book.

### Account

| Intent | Method |
| --- | --- |
| Adjust Margin | `client.FuturesApi.Account.AdjustMarginAsync(...)` |
| Get Balances | `client.FuturesApi.Account.GetBalancesAsync(...)` |
| Get Trading Settings | `client.FuturesApi.Account.GetTradingSettingsAsync(...)` |
| Get Position Mode | `client.FuturesApi.Account.GetPositionModeAsync(...)` |
| Get Leverage Margin Mode | `client.FuturesApi.Account.GetLeverageMarginModeAsync(...)` |
| Set Position Mode | `client.FuturesApi.Account.SetPositionModeAsync(...)` |
| Set Margin Mode | `client.FuturesApi.Account.SetMarginModeAsync(...)` |
| Set Leverage | `client.FuturesApi.Account.SetLeverageAsync(...)` |

### Trading

| Intent | Method |
| --- | --- |
| Place Order | `client.FuturesApi.Trading.PlaceOrderAsync(...)` |
| Edit Order | `client.FuturesApi.Trading.EditOrderAsync(...)` |
| Cancel Order | `client.FuturesApi.Trading.CancelOrderAsync(...)` |
| Cancel Orders | `client.FuturesApi.Trading.CancelOrdersAsync(...)` |
| Cancel All Orders | `client.FuturesApi.Trading.CancelAllOrdersAsync(...)` |
| Close All Positions | `client.FuturesApi.Trading.CloseAllPositionsAsync(...)` |
| Close Position | `client.FuturesApi.Trading.ClosePositionAsync(...)` |
| Get Order History | `client.FuturesApi.Trading.GetOrderHistoryAsync(...)` |
| Get Order | `client.FuturesApi.Trading.GetOrderAsync(...)` |
| Get Open Orders | `client.FuturesApi.Trading.GetOpenOrdersAsync(...)` |
| Get User Trades | `client.FuturesApi.Trading.GetUserTradesAsync(...)` |
| Get Positions | `client.FuturesApi.Trading.GetPositionsAsync(...)` |
| Get Position History | `client.FuturesApi.Trading.GetPositionHistoryAsync(...)` |
| Place Tp Sl Order | `client.FuturesApi.Trading.PlaceTpSlOrderAsync(...)` |
| Place Position Tp Sl Order | `client.FuturesApi.Trading.PlacePositionTpSlOrderAsync(...)` |
| Cancel Tp Sl Order | `client.FuturesApi.Trading.CancelTpSlOrderAsync(...)` |
| Get Open Tp Sl Orders | `client.FuturesApi.Trading.GetOpenTpSlOrdersAsync(...)` |
| Get Tp Sl Order History | `client.FuturesApi.Trading.GetTpSlOrderHistoryAsync(...)` |

The TP/SL and history methods have long signatures. Use the checkout-only [futures trading interface](../../../../Bitunix.Net/Interfaces/Clients/FuturesApi/IBitunixRestClientFuturesApiTrading.cs) for exact overloads instead of inferring argument order.

## Socket Operations

| Intent | Method |
| --- | --- |
| Place Order | `socketClient.SpotApi.PlaceOrderAsync(...)` |
| Cancel Order | `socketClient.SpotApi.CancelOrderAsync(...)` |
| Cancel Orders | `socketClient.SpotApi.CancelOrdersAsync(...)` |

These return `QueryResult<T>` or `QueryResult`; they are not subscriptions. The current Spot socket exposes no public market or user-data streams.

| Intent | Method |
| --- | --- |
| Subscribe To Ticker Updates | `socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(...)` |
| Subscribe To Aggregated Ticker Updates | `socketClient.FuturesApi.SubscribeToAggregatedTickerUpdatesAsync(...)` |
| Subscribe To Order Book Updates | `socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(...)` |
| Subscribe To Kline Updates | `socketClient.FuturesApi.SubscribeToKlineUpdatesAsync(...)` |
| Subscribe To Mark Price Updates | `socketClient.FuturesApi.SubscribeToMarkPriceUpdatesAsync(...)` |
| Subscribe To Trade Updates | `socketClient.FuturesApi.SubscribeToTradeUpdatesAsync(...)` |

Each public Futures subscription has single-symbol and multi-symbol overloads. Socket order-book limits are 1, 5, or 15 for snapshots; null requests incremental updates.

Private Futures subscription:

- `socketClient.FuturesApi.SubscribeToUserUpdatesAsync(onBalanceMessage, onOrderMessage, onPositionMessage, onTpSlMessage)`

Private authentication uses socket client credentials directly. There is no listen-key endpoint or lifecycle.

## Result Types

- REST: `HttpResult<T>` or `HttpResult`
- Futures subscriptions: `WebSocketResult<UpdateSubscription>`
- Spot socket queries: `QueryResult<T>` or `QueryResult`
- Shared API methods: capability-specific CryptoExchange.Net result types

Check `Success` before using `Data`. Only successful Futures/shared subscriptions produce an `UpdateSubscription` for `UnsubscribeAsync`.

## Shared API V2

Use these surfaces for Shared API V2:

- `restClient.SpotApi.SharedApi`
- `restClient.FuturesApi.SharedApi`
- `socketClient.SpotApi.SharedApi`
- `socketClient.FuturesApi.SharedApi`

The `IBitunixSharedApiClient` aggregate exposes `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket`.

Spot REST capabilities include `IGetAllAssetsRest`, `IGetBalancesRest`, `IGetSpotSymbolsRest`, `IPlaceSpotOrderRest`, and `ICancelSpotOrderRest`:

- Assets, balances, deposit history/addresses, klines, order book, Spot symbols, withdrawal history/placement
- Spot order placement, lookup, open/closed orders, trades, and cancellation
- No `IGetTickerRest`; native Spot only exposes last price

Futures REST capabilities include `IGetTickerRest`, `IGetFuturesSymbolsRest`, `IPlaceFuturesOrderRest`, `IGetPositionsRest`, `ISetFuturesTpSlRest`, and `ICancelFuturesTpSlRest`:

- Balances, funding info/history, mark prices, klines and mark-price klines
- Leverage get/set and tiers, book ticker/order book, position mode get/set, Futures symbols, one/all tickers
- Futures order place/get/edit/cancel, client-order-ID variants, open/closed orders, trades and user-trade history, cancel-all
- Positions/history and Futures TP/SL set/cancel

Spot socket capabilities:

- `IPlaceSpotOrderSocket`
- `ICancelSpotOrderSocket`

Futures socket capabilities include `ISubscribeTickerSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeFuturesOrdersSocket`, and `ISubscribePositionsSocket`:

- Trades, ticker, klines, order book, funding info, mark price, index price
- Balances, Futures orders, and positions

Depend on the narrowest capability needed. Do not assume a capability exists because another exchange implements it.

## Symbols And Enums

- Spot and Futures native symbols: `ETHUSDT`
- Shared Spot: `new SharedSymbol(TradingMode.Spot, "ETH", "USDT")`
- Shared Futures: `TradingMode.PerpetualLinear` or `TradingMode.PerpetualInverse`
- Spot/Futures sides: `OrderSide.Buy`, `OrderSide.Sell`
- Order types: `OrderType.Limit`, `OrderType.Market`
- Hedge intent: `TradeSide.Open`, `TradeSide.Close`
- Position mode: `PositionMode.HedgeMode`, `PositionMode.OneWay`
- Margin mode: `MarginMode.Isolated`, `MarginMode.Cross`
- REST Futures klines: `FuturesKlineInterval`
- Socket Futures klines: `FuturesSocketKlineInterval`

## DI And Environments

`services.AddBitunix(...)` registers REST/socket clients, the SharedApi aggregate and strict interfaces, order-book/tracker factories, and user client provider.

Only `BitunixEnvironment.Live` is built in. `BitunixEnvironment.CreateCustom(...)` requires name plus four endpoint addresses: Spot REST, Spot socket, Futures REST, and Futures socket.


## Dynamic selection and symbol catalogs

Use `IBitunixSharedApiClient.GetCapability` with a descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode when choosing an implementation dynamically. Check for a missing match before accessing its `Capability` and `Options`. `GetCapabilities` enumerates implementations; `Discover()` describes a surface's operations.

Use `IGetSpotSymbolsRest` on `SpotRest` and `IGetFuturesSymbolsRest` on `FuturesRest`. After a successful `GetSpotSymbolsAsync(new GetSymbolsRequest())` or `GetFuturesSymbolsAsync(new GetSymbolsRequest())`, read `SpotSymbolCatalog` or `FuturesSymbolCatalog`. Requests support base/quote asset type and subtype filters; symbols include `DisplayName`, asset types (`Crypto`, `Fiat`, `TradFi`), and applicable subtypes (`StableCoin`, `Equity`, `Commodity`).

Shared orders use `SharedOrderQuantity`; check the capability's options for accepted representations rather than passing a native quantity unconditionally. Spot market buys use quote-asset quantity; spot limit orders and market sells use base-asset quantity. Futures order capabilities use base-asset quantities. Shared REST and futures socket order books report `SharedQuantityType.BaseAsset`.

Shared REST results use `HttpResult<T>` / `HttpResult`, socket order queries use `QueryResult<T>` / `QueryResult`, subscription calls use `WebSocketResult<UpdateSubscription>`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

## Source navigation (checkout-only)

- [Public interfaces](../../../../Bitunix.Net/Interfaces/Clients/) define exact native overloads and Shared API capability inheritance.
- [Local order books](../../../../Bitunix.Net/SymbolOrderBooks/) and [tracker factory](../../../../Bitunix.Net/BitunixTrackerFactory.cs) provide synchronized futures book and account tracking workflows.
