using Bitunix.Net.Interfaces.Clients;
using Bitunix.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitunix.Net.Clients
{
    internal class BitunixSharedApiClient : SharedApiClientBase, IBitunixSharedApiClient
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BitunixSharedApiClient(
            IBitunixRestClient restClient,
            IBitunixSocketClient socketClient,
            IOptions<BitunixOptions> options)
            : base(options.Value.SharedApi.PreferredTransport
#warning TODO
                )
        {
        }
    }
}
