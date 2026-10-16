using CryptoExchange.Net.Objects.Options;

namespace Bitunix.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitunixRestClient
    /// </summary>
    public class BitunixRestOptions : RestExchangeOptions<BitunixEnvironment, BitunixCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static BitunixRestOptions Default { get; set; } = new BitunixRestOptions()
        {
            Environment = BitunixEnvironment.Live,
            AutoTimestamp = false
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixRestOptions()
        {
            Default?.Set(this);
        }

                 /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();


        internal BitunixRestOptions Set(BitunixRestOptions targetOptions)
        {
            targetOptions = base.Set<BitunixRestOptions>(targetOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
