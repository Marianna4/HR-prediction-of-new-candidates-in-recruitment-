using System;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Transfer_data_from_csv.Helpers;

namespace Transfer_data_from_csv.Services
{
    class AuthenticateClientCSService
    {
        public TextAnalyticsClient authenticateClient()
        {          
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(ConstantHelper.key);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = ConstantHelper.endpoint.ToString()
            };
            return client;
        }
    }
}
