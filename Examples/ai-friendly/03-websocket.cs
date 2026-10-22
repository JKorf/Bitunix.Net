// 03-websocket.cs
//
// Demonstrates: public Futures subscriptions, optional authenticated Futures
// user updates, and proper teardown. The current Spot socket exposes order
// queries, not market-data subscriptions.
//
// Setup: dotnet add package Bitunix.Net

using Bitunix.Net;
using Bitunix.Net.Clients;

using var publicSocket = new BitunixSocketClient();

var tickerSub = await publicSocket.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update => Console.WriteLine(
        $"{update.Data.Symbol}: last={update.Data.LastPrice}, " +
        $"24h change={update.Data.PriceChangePercentage}%"));

if (!tickerSub.Success)
{
    Console.WriteLine($"Ticker subscription failed: {tickerSub.Error}");
    return;
}

var tradeSub = await publicSocket.FuturesApi.SubscribeToTradeUpdatesAsync(
    "ETHUSDT",
    update =>
    {
        foreach (var trade in update.Data)
            Console.WriteLine($"Trade: {trade.Price} x {trade.Quantity}");
    });

if (!tradeSub.Success)
{
    Console.WriteLine($"Trade subscription failed: {tradeSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

BitunixSocketClient? privateSocket = null;
CryptoExchange.Net.Objects.Sockets.UpdateSubscription? userSubscription = null;

var apiKey = Environment.GetEnvironmentVariable("BITUNIX_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("BITUNIX_API_SECRET");

if (!string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(apiSecret))
{
    privateSocket = new BitunixSocketClient(options =>
    {
        options.ApiCredentials = new BitunixCredentials(apiKey, apiSecret);
    });

    var userSub = await privateSocket.FuturesApi.SubscribeToUserUpdatesAsync(
        onBalanceMessage: update =>
            Console.WriteLine($"Balance {update.Data.Asset}: {update.Data.Available}"),
        onOrderMessage: update =>
            Console.WriteLine($"Order {update.Data.OrderId}: {update.Data.Status}"),
        onPositionMessage: update =>
            Console.WriteLine($"Position {update.Data.PositionId}: {update.Data.Quantity}"),
        onTpSlMessage: update =>
            Console.WriteLine($"TP/SL update: {update.Data.Event}"));

    if (userSub.Success)
    {
        userSubscription = userSub.Data;
    }
    else
    {
        Console.WriteLine($"Private subscription failed: {userSub.Error}");
        privateSocket.Dispose();
        privateSocket = null;
    }
}

Console.WriteLine("Subscriptions active. Press Enter to stop.");
Console.ReadLine();

await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(tradeSub.Data);

if (privateSocket is not null && userSubscription is not null)
{
    await privateSocket.UnsubscribeAsync(userSubscription);
    privateSocket.Dispose();
}

Console.WriteLine("All subscriptions stopped.");
