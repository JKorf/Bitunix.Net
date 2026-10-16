using CryptoExchange.Net.Objects.Options;

namespace Bitunix.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitunixSocketClient
    /// </summary>
    public class BitunixSocketOptions : SocketExchangeOptions<BitunixEnvironment, BitunixCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static BitunixSocketOptions Default { get; set; } = new BitunixSocketOptions()
        {
            Environment = BitunixEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitunixSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        internal BitunixSocketOptions Set(BitunixSocketOptions targetOptions)
        {
            targetOptions = base.Set<BitunixSocketOptions>(targetOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
