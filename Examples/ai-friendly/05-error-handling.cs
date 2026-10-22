// 05-error-handling.cs
//
// Demonstrates: HttpResult handling, bounded retry for transient failures, and
// symbol-rule validation before a guarded live Spot order.
//
// Setup: dotnet add package Bitunix.Net

using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;
using CryptoExchange.Net.Objects;

using var publicClient = new BitunixRestClient();

var price = await publicClient.SpotApi.ExchangeData
    .GetLastPriceAsync("ETHUSDT");

if (!price.Success)
{
    Console.WriteLine($"Code:      {price.Error?.Code}");
    Console.WriteLine($"Message:   {price.Error?.Message}");
    Console.WriteLine($"Type:      {price.Error?.ErrorType}");
    Console.WriteLine($"Transient: {price.Error?.IsTransient}");
}
else
{
    Console.WriteLine($"ETHUSDT: {price.Data}");
}

// Retry only errors marked transient, and keep the attempt count bounded.
static async Task<HttpResult<T>> WithRetryAsync<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success || last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * attempt));
    }

    return last;
}

var symbols = await WithRetryAsync(
    () => publicClient.SpotApi.ExchangeData.GetSymbolsAsync());

if (!symbols.Success)
{
    Console.WriteLine($"Symbol request failed: {symbols.Error}");
    return;
}

var symbol = symbols.Data.SingleOrDefault(x => x.Symbol == "ETHUSDT");
if (symbol is null || !symbol.IsOpen)
{
    Console.WriteLine("ETHUSDT is unavailable for Spot trading.");
    return;
}

var rawQuantity = 0.12345678m;
var rawPrice = 2000.123456m;

// Bitunix exposes decimal-place precision on the Spot symbol model.
var quantity = Math.Round(
    rawQuantity,
    symbol.BaseAssetPrecision,
    MidpointRounding.ToZero);
var limitPrice = Math.Round(
    rawPrice,
    symbol.QuoteAssetPrecision,
    MidpointRounding.ToZero);

if (quantity < symbol.MinOrderQuantity ||
    quantity * limitPrice < symbol.MinOrderValue)
{
    Console.WriteLine("Order does not satisfy the symbol minimums.");
    return;
}

var apiKey = Environment.GetEnvironmentVariable("BITUNIX_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("BITUNIX_API_SECRET");

// No test-order endpoint exists. Keep live placement explicitly disabled until
// credentials, balance, symbol rules, and user intent have all been verified.
var placeLiveOrder = false;
if (!placeLiveOrder ||
    string.IsNullOrWhiteSpace(apiKey) ||
    string.IsNullOrWhiteSpace(apiSecret))
{
    Console.WriteLine("Validated order parameters; live order skipped.");
    return;
}

using var privateClient = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials(apiKey, apiSecret);
});

var order = await privateClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: symbol.Symbol,
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: quantity,
    price: limitPrice);

if (!order.Success)
{
    var action = order.Error?.IsTransient == true
        ? "Reconcile the order state, then retry with bounded backoff."
        : "Correct the request before retrying.";

    Console.WriteLine($"Order failed: {order.Error}. {action}");
}
