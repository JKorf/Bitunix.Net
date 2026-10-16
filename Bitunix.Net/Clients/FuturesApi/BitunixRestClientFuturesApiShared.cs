using CryptoExchange.Net.SharedApis;
using Bitunix.Net.Interfaces.Clients.FuturesApi;

namespace Bitunix.Net.Clients.FuturesApi
{
    internal partial class BitunixRestClientFuturesApi : IBitunixRestClientFuturesApiShared
    {
        private const string _topicId = "BitunixFutures";
        private const string _exchangeName = "Bitunix";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitunixExchange.Metadata, this);
    }
}
