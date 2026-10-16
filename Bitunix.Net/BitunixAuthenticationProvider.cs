using Bitunix.Net.Objects.Internal;
using Bitunix.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bitunix.Net
{
    internal class BitunixAuthenticationProvider : AuthenticationProvider<BitunixCredentials, BitunixCredentials>
    {
        private static readonly IStringMessageSerializer _messageSerializer = new SystemTextJsonMessageSerializer(BitunixExchange._serializerContext);

        public BitunixAuthenticationProvider(BitunixCredentials credentials) : base(credentials, credentials)
        {
        }


        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration requestConfig)
        {
            if (!requestConfig.RequestDefinition.Authenticated)
                return;

            var nonce = ExchangeHelpers.RandomString(4);
            var timestamp = GetMillisecondTimestamp(apiClient);
            var signStr = nonce + timestamp + Key;
            if (requestConfig.QueryParameters != null && requestConfig.QueryParameters.Count > 0) {
                foreach(var queryParam in requestConfig.QueryParameters.OrderBy(x => x.Key))
                    signStr += queryParam.Key + queryParam.Value;
            }

            if (requestConfig.BodyParameters != null && !requestConfig.BodyParameters.Empty) 
            {
                var bodyString = GetSerializedBody(_messageSerializer, requestConfig.BodyParameters);
                signStr += bodyString;
                requestConfig.SetBodyContent(bodyString);
            }

            var sign = SignSHA256(signStr).ToLowerInvariant();
            sign = SignSHA256(sign + Credential.Secret).ToLowerInvariant();

            requestConfig.Headers ??= new Dictionary<string, string>();
            requestConfig.Headers.Add("api-key", Key);
            requestConfig.Headers.Add("nonce", nonce);
            requestConfig.Headers.Add("timestamp", timestamp);
            requestConfig.Headers.Add("sign", sign);
        }

        public void AddAuth(SocketApiClient apiClient, BitunixSocketRequest request)
        {
            var nonce = ExchangeHelpers.RandomString(4);
            var timestamp = GetMillisecondTimestamp(apiClient);
            var signStr = nonce + timestamp + Key;
            foreach (var param in request.Params.OrderBy(x => x.Key))
            {
                signStr += param.Key;
                if (param.Value is Array array)
                {
                    // For complicated/array values we need to sign the json value it contains
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
                    signStr += JsonSerializer.Serialize(param.Value, BitunixExchange._serializerContext);
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
                }
                else
                {
                    signStr += param.Value;
                }
            }

            var sign = SignSHA256(signStr).ToLowerInvariant();
            sign = SignSHA256(sign + Credential.Secret).ToLowerInvariant();

            request.Params.Add("apiKey", Key);
            request.Params.Add("nonce", nonce);
            request.Params.Add("timestamp", timestamp);
            request.Params.Add("sign", sign);
        }
    }
}
