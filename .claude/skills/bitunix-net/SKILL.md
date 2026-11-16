---
name: bitunix-net
description: Implement or review Bitunix.Net spot and futures REST calls, account and trading workflows, futures WebSocket subscriptions, spot socket order queries, authentication, Shared API integration, and dependency injection in .NET applications.
---
# Bitunix.Net

Install with `dotnet add package Bitunix.Net`.

- Reuse `BitunixRestClient` and `BitunixSocketClient`, or resolve them through dependency injection. Both expose `SpotApi` and `FuturesApi`.
- Private operations use `BitunixCredentials(key, secret)` with no passphrase. Public market data needs no credentials.
- Native symbols are compact uppercase pairs such as `ETHUSDT`. Select an available market from the relevant symbol list.
- REST returns `HttpResult<T>` or `HttpResult`. Futures subscriptions return `WebSocketResult<UpdateSubscription>`. Spot socket order queries return `QueryResult<T>` or `QueryResult`. Check `Success` before reading `Data`, and handle `Error` on failure.
- Futures sockets provide public market and private user subscriptions. Spot sockets provide authenticated order placement/cancellation queries, with no market or user subscriptions. Unsubscribe successful subscriptions through the concrete socket client; query results require no unsubscribe.
- Private futures sockets authenticate with the configured credentials; there is no listen-key lifecycle.
- Direct spot orders accept symbol, side, type, quantity, and optional price, with no time-in-force or client-order-ID parameters. Futures orders have separate mode and position semantics.
- Use each API root's `SharedApi` for Shared API V2 and depend on the required capability interface. Spot REST has no shared ticker capability; use native `GetLastPriceAsync` for a spot price.

## References

- Read [API surfaces](references/api-surfaces.md) to select native methods, Shared API capabilities, symbols, and quantity representations.
- Read [usage](references/usage.md) for authentication, market/account calls, trading, socket lifecycle, Shared API V2, and DI snippets.
- Read [operational considerations](references/safety.md) for position modes, batch outcomes, withdrawals, and submission recovery.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).
