// 04-multi-exchange.cs
//
// Demonstrates: exchange-independent Futures ticker access through strict
// CryptoExchange.Net SharedApis capabilities.
//
// Setup: dotnet add package Bitunix.Net

using Bitunix.Net.Clients;
using CryptoExchange.Net.SharedApis;

using var bitunix = new BitunixRestClient();

// Depend on the narrow operation needed by the application. Another exchange
// can supply its own IGetTickerRest implementation to the same function.
IGetTickerRest tickerClient = bitunix.FuturesApi.SharedApi;

var symbol = new SharedSymbol(
    TradingMode.PerpetualLinear,
    "ETH",
    "USDT");

await PrintTickerAsync(tickerClient, symbol);

static async Task PrintTickerAsync(
    IGetTickerRest client,
    SharedSymbol symbol)
{
    var result = await client.GetTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"Ticker request failed: {result.Error}");
        return;
    }

    Console.WriteLine(
        $"{result.Data.Symbol}: {result.Data.LastPrice}");
}

// Bitunix Shared API surfaces:
//   bitunix.SpotApi.SharedApi      Spot symbols, balances, orders, klines, etc.
//   bitunix.FuturesApi.SharedApi   Futures tickers, orders, positions, funding.
//   socket.SpotApi.SharedApi       Spot order placement/cancellation queries.
//   socket.FuturesApi.SharedApi    Futures market and private subscriptions.
//
// Spot REST intentionally has no IGetTickerRest capability because the native
// endpoint exposes only GetLastPriceAsync. Use the direct Spot API for that.
