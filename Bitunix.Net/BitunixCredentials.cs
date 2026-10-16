using CryptoExchange.Net.Authentication;
using System;

namespace Bitunix.Net
{
    /// <summary>
    /// Bitunix API credentials
    /// </summary>
    public class BitunixCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public BitunixCredentials() { }

        /// <summary>
        /// Create new credentials providing credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BitunixCredentials(string key, string secret) : base(key, secret)
        {
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public BitunixCredentials(HMACCredential credential) : base(credential.Key, credential.Secret)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BitunixCredentials WithHMAC(string key, string secret)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BitunixCredentials(this);
    }
}
