using Bitunix.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Bitunix.Net.Objects.Options;
using Microsoft.Extensions.Options;

// REST
var restClient = new BitunixRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetLastPriceAsync("ETHUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

// Note that Spot currently does not offer a WebSocket API
var socketClient = new BitunixSocketClient(Options.Create(new BitunixSocketOptions { }), logFactory);
var subscription = await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
