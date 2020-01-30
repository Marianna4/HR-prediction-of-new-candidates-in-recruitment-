using System;
using System.Collections.Generic;
using Transfer_data_from_csv.Services;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
using System.Threading.Tasks;
using Azure.AI.TextAnalytics;
namespace Transfer_data_from_csv
{
    class Program
    {
        static async Task Main(string[] args)
        { 
            //Read from file
            ReadFromFile CsvFile = new ReadFromFile();
            var entities= CsvFile.FileReading() ;
                             
            var dataService = new AnswerDataService();
            try
            {
                await dataService.InsertData(entities, ConstantHelper.accountName, ConstantHelper.accountKey);
            }
            catch (Exception ex)
            {
                var v = ex;
            }

           //Reading from Table
            DataFromTableService tableData = new DataFromTableService();
            var DataTable = new  List<AnswerEntities>();
            try
            {
               DataTable = await tableData.OutputData( ConstantHelper.accountName, ConstantHelper.accountKey);
            }
            catch (Exception ex)
            {
                var v = ex;
            }
            //foreach (AnswerEntities data in DataTable)
            //{
            //    Console.WriteLine("{0}, {1}\t{2}\t{3}\t{4}",data.PartitionKey, data.RowKey,
            //                             data.Name, data.Answer, data.Email);
            //}
            ///Work with cognitiveServices
            var client = new TextAnalyticsClient (ConstantHelper.endpoint, ConstantHelper.key);
            var analysText = new TextAnalysicService();
          //  analysText.languageDetectionExample(client, DataTable[0].Answer);
            analysText.SentimentAnalysisExample(client, DataTable[0].Answer);
            analysText.KeyPhraseExtractionExample(client, DataTable[0].Answer);
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

    }

}

