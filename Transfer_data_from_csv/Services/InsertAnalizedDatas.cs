using System;
using System.Collections.Generic;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
using Azure.AI.TextAnalytics;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Threading.Tasks;

namespace Transfer_data_from_csv.Services
{
    class InsertAnalizedDatas
    {
        public static ContentModeratorClient Authenticate(string key, string endpoint)
        {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;
            return client;
        }

        public List<AnalyzedDatasEntities> InserTAfterAnalized(List<AnswerEntities> DataTable)
        {
            var client = new TextAnalyticsClient(ConstantHelper.endpoint, ConstantHelper.key);

            ContentModeratorClient clientText = new ContentModeratorClient(new ApiKeyServiceClientCredentials(ConstantHelper.ApiKey));
            clientText.Endpoint = ConstantHelper.endpoint.OriginalString;

            var analysText = new TextAnalysicService();
            var processedData = new List<AnalyzedDatasEntities>();

            for (int i = 0; i < DataTable.Count; i++)
            {
                var tempEntity = new AnalyzedDatasEntities()
                {
                    PartitionKey = DataTable[i].PartitionKey,
                    RowKey = DataTable[i].RowKey,
                    Name = DataTable[i].Name,
                    Email = DataTable[i].Email,
                    IsProcessed = true,
                    Q1Reply = DataTable[i].Answer1,                  
                    Q1Lenghth = DataTable[i].Answer1.Length,
                    Q1Language = analysText.LanguageDetectionExample(client, DataTable[i].Answer1),
                    Q1Sentiment = analysText.SentimentAnalysisExample(client, DataTable[i].Answer1),
                    Q1KeyPhrases = analysText.KeyPhraseExtractionExample(client, DataTable[i].Answer1),
                    Q1ProfanityTerms = analysText.ModerateText(clientText, DataTable[i].Answer1),

                    Q2Reply = DataTable[i].Answer2,
                    Q2Lenghth = DataTable[i].Answer2.Length,
                    Q2Language = analysText.LanguageDetectionExample(client, DataTable[i].Answer2),
                    Q2Sentiment = analysText.SentimentAnalysisExample(client, DataTable[i].Answer2),
                    Q2KeyPhrases = analysText.KeyPhraseExtractionExample(client, DataTable[i].Answer2),
                    Q2ProfanityTerms = analysText.ModerateText(clientText, DataTable[i].Answer2),

                    Q3Reply = DataTable[i].Answer3,
                    Q3Lenghth = DataTable[i].Answer3.Length,
                    Q3Language = analysText.LanguageDetectionExample(client, DataTable[i].Answer3),
                    Q3Sentiment = analysText.SentimentAnalysisExample(client, DataTable[i].Answer3),
                    Q3KeyPhrases = analysText.KeyPhraseExtractionExample(client, DataTable[i].Answer3),
                    Q3ProfanityTerms = analysText.ModerateText(clientText, DataTable[i].Answer3),

                    Q4Reply = DataTable[i].Answer4,
                    Q4Lenghth = DataTable[i].Answer4.Length,
                    Q4Language = analysText.LanguageDetectionExample(client, DataTable[i].Answer4),
                    Q4Sentiment = analysText.SentimentAnalysisExample(client, DataTable[i].Answer4),
                    Q4KeyPhrases = analysText.KeyPhraseExtractionExample(client, DataTable[i].Answer4),
                    Q4ProfanityTerms = analysText.ModerateText(clientText, DataTable[i].Answer4),

                };
                processedData.Add(tempEntity);
            }
            return processedData;
        }
    }
}
