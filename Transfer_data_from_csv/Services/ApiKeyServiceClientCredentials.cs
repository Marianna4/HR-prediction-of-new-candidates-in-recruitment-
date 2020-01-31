using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Transfer_data_from_csv.Helpers;
namespace Transfer_data_from_csv.Services
{
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string apiKey;
        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }            
            request.Headers.Add(ConstantHelper.key, this.apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
