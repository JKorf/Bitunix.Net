using CryptoExchange.Net;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Converters
{
    internal class MarketPriceConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                return null;

            var str = reader.GetString()!;
            if (str.Equals("MARKET"))
                return null;

            return ExchangeHelpers.ParseDecimal(str);
        }
        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString() ?? "MARKET");
        }
    }
}
