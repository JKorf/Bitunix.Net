using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Interfaces.Clients.SpotApi;

namespace Bitunix.Net.Clients.SpotApi
{
    internal partial class BitunixSocketClientSpotApi : IBitunixSocketClientSpotApiShared
    {
        private const string _topicId = "BitunixSpot";
        private const string _exchangeName = "Bitunix";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);
    }
}
