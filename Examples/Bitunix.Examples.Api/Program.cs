using Bitunix.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Bitunix services
builder.Services.AddBitunix();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddBitunix(options =>
{
    options.ApiCredentials = new BitunixCredentials("<APIKEY>", "<APISECRET>");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoints and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IBitunixRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetLastPriceAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
});

app.MapGet("/Balances", async ([FromServices] IBitunixRestClient client) =>
{
    var result = await client.SpotApi.Account.GetBalancesAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
});

app.Run();