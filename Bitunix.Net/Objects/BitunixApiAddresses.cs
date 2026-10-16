namespace Bitunix.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class BitunixApiAddresses
    {
        /// <summary>
        /// The address used by the BitunixRestClient for the Spot API
        /// </summary>
        public string SpotRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitunixSocketClient for the websocket Spot API
        /// </summary>
        public string SpotSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Bitunix API
        /// </summary>
        public static BitunixApiAddresses Default = new BitunixApiAddresses
        {
            SpotRestClientAddress = "https://openapi.bitunix.com",
            SpotSocketClientAddress = "wss://openapi.bitunix.com:443"
        };
    }
}
