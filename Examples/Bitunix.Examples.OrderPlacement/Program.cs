using Bitunix.Net;
using Bitunix.Net.Clients;
using Bitunix.Net.Enums;

const string spotSymbol = "BTCUSDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "3a73aed9d5bd615c7cd9a703d7a51235";
var apiSecret = "c49754ed6bc7634fa4cdf9146aca0f3d";

Console.WriteLine("Bitunix.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new BitunixRestClient(options =>
{
    options.ApiCredentials = new BitunixCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);

static async Task PlaceSpotLimitOrderAsync(BitunixRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.SpotApi.ExchangeData.GetLastPriceAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data * 0.95m, 1);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        orderSide: OrderSide.Buy,
        orderType: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.FilledQuantity}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(spotSymbol, orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}
