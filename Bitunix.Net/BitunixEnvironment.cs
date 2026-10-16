using CryptoExchange.Net.Objects;
using Bitunix.Net.Objects;

namespace Bitunix.Net
{
    /// <summary>
    /// Bitunix environments
    /// </summary>
    public class BitunixEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API address
        /// </summary>
        public string SpotRestClientAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SpotSocketClientAddress { get; }

        internal BitunixEnvironment(
            string name,
            string spotRestAddress,
            string spotStreamAddress) :
            base(name)
        {
            SpotRestClientAddress = spotRestAddress;
            SpotSocketClientAddress = spotStreamAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BitunixEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Bitunix environment by name
        /// </summary>
        public static BitunixEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static BitunixEnvironment Live { get; }
            = new BitunixEnvironment(TradeEnvironmentNames.Live,
                                     BitunixApiAddresses.Default.SpotRestClientAddress,
                                     BitunixApiAddresses.Default.SpotSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <returns></returns>
        public static BitunixEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress)
            => new BitunixEnvironment(name, spotRestAddress, spotSocketStreamsAddress);
    }
}
