﻿using System;
using System.Collections.Generic;
using System.Text;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
using Azure.AI.TextAnalytics;
using Microsoft.Azure.CognitiveServices.ContentModerator;

namespace Transfer_data_from_csv.Services
{
    class InsertAnalizedDatas
    {
       public List<AnalyzedDatasEntities> InserTAfterAnalized(List<AnswerEntities> DataTable)
        {
            var client = new TextAnalyticsClient(ConstantHelper.endpoint, ConstantHelper.key);
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
                    Q1Reply = DataTable[i].Answer,
                    IsProcessed = true,
                    Q1Lenghth = DataTable[i].Answer.Length,
                    Q1Language = analysText.LanguageDetectionExample(client, DataTable[i].Answer),
                    Q1Sentiment = analysText.SentimentAnalysisExample(client, DataTable[i].Answer),
                    Q1KeyPhrases = analysText.KeyPhraseExtractionExample(client, DataTable[i].Answer)
                };
                processedData.Add(tempEntity);

            }
            return processedData;
        }
    }
}