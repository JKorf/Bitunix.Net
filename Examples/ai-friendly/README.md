# AI-Friendly Bitunix.Net Examples

These self-contained console programs demonstrate the current Bitunix.Net API
surface. Each file can replace `Program.cs` in a console project after adding
the package:

```bash
dotnet new console -n MyBitunixApp
cd MyBitunixApp
dotnet add package Bitunix.Net
```

| File | Demonstrates |
| --- | --- |
| `01-unified-quickstart.cs` | Public Spot data, credentials, balances, guarded Spot orders |
| `02-derivatives.cs` | Futures tickers, balances, positions, leverage, guarded orders |
| `03-websocket.cs` | Public and private Futures subscriptions with teardown |
| `04-multi-exchange.cs` | Strict CryptoExchange.Net SharedApis ticker capability |
| `05-error-handling.cs` | Result errors, transient retry, and symbol validation |

Private examples read `BITUNIX_API_KEY` and `BITUNIX_API_SECRET` from the
environment. Live order and leverage mutations are disabled by constants;
review symbol rules, balances, account modes, and risk before enabling them.

Bitunix.Net currently exposes Futures market/user subscriptions. Its Spot
socket surface provides authenticated order queries rather than market streams.
