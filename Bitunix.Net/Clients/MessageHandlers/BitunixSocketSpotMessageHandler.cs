using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Bitunix.Net.Clients.MessageHandlers
{
    internal class BitunixSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = BitunixExchange._serializerContext;

        public BitunixSocketSpotMessageHandler()
        {
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
