using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Converters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters;

namespace Bitunix.Net
{
    /// <summary>
    /// Bitunix exchange information and configuration
    /// </summary>
    public static class BitunixExchange
    {
        internal static JsonSerializerOptions _serializerContext = SerializerOptions.WithConverters(JsonSerializerContextCache.GetOrCreate<BitunixSourceGenerationContext>());
        internal static ParameterSerializationSettings _parameterSerializationSettings = new ParameterSerializationSettings
        {
            Decimal = DecimalSerialization.String,
            DateTimes = DateTimeSerialization.MillisecondsString
        };

        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Bitunix",
                "Bitunix",
                "https://raw.githubusercontent.com/JKorf/Bitunix.Net/main/Bitunix.Net/Icon/icon.png",
                "https://www.bitunix.com",
                ["https://www.bitunix.com/api-docs/spots/en_us/",
                 "https://www.bitunix.com/api-docs/futures/common/introduction.html"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized,
                BitunixEnvironment.All
                );

        /// <summary>
        /// Aliases for Bitunix assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases = [
                new AssetAlias("USDT", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to an Bitunix recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            return baseAsset + quoteAsset;
        }

        /// <summary>
        /// Rate limiter configuration for the Bitunix API
        /// </summary>
        public static BitunixRateLimiters RateLimiter { get; } = new BitunixRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Bitunix API
    /// </summary>
    public class BitunixRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;
        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BitunixRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            // Rate limits are enforced per endpoint
            Bitunix = new RateLimitGate("Bitunix");
            Bitunix.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Bitunix.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate Bitunix { get; private set; }

    }
}
