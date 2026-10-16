using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using Bitunix.Net.Clients;

namespace Bitunix.Net.UnitTests
{
    [TestFixture()]
    public class BitunixRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BitunixAuthenticationProvider(new BitunixCredentials("XXX", "XXX"));
            var client = (RestApiClient)new BitunixRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return bodyParams["signature"].ToString();
                },
                "c8db56825ae71d6d79447849e617115f4a920fa2acdcab2b053c4b2838bd6b71",
                new Parameters(BitunixExchange._parameterSerializationSettings)
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitunixRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitunixSocketClient>();
        }
    }
}
