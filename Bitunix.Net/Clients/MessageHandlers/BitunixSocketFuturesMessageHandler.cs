using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Text.Json;

namespace Bitunix.Net.Clients.MessageHandlers
{
    internal class BitunixSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = BitunixExchange._serializerContext;

        public BitunixSocketFuturesMessageHandler()
        {
            AddTopicMapping<BitunixFuturesSocketUpdate<BitunixTickerUpdate>>(x => x.Symbol);
            AddTopicMapping<BitunixFuturesSocketUpdate<BitunixOrderBookUpdate>>(x => x.Symbol);
            AddTopicMapping<BitunixFuturesSocketUpdate<BitunixKlineUpdate>>(x => x.Symbol);
            AddTopicMapping<BitunixFuturesSocketUpdate<BitunixMarkPriceUpdate>>(x => x.Symbol);
            AddTopicMapping<BitunixFuturesSocketUpdate<BitunixTradeUpdate[]>>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("ch"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("ch")!,
            },
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("op").WithEqualConstraint("ping"),
                    new PropertyFieldReference("ping"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("ping")!,
            }
        ];
    }
}
