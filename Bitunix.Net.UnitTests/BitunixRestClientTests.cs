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
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitunixRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitunixSocketClient>();
        }

        [Test]
        public void TestRestSpotSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new BitunixRestClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSocketSpotSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new BitunixSocketClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = CryptoExchange.Net.Testing.TestHelpers.ValidateUnsupportedCapabilities(new BitunixRestClient().SpotApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestSpotSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = CryptoExchange.Net.Testing.TestHelpers.ValidateUnsupportedCapabilities(new BitunixSocketClient().SpotApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestRestFuturesSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new BitunixRestClient().FuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSocketFuturesSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new BitunixSocketClient().FuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestFuturesRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = CryptoExchange.Net.Testing.TestHelpers.ValidateUnsupportedCapabilities(new BitunixRestClient().FuturesApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestFuturesSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = CryptoExchange.Net.Testing.TestHelpers.ValidateUnsupportedCapabilities(new BitunixSocketClient().FuturesApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }
    }
}
