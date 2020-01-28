using System;
using System.Collections.Generic;
using System.Text;
using Transfer_data_from_csv.Entities;
using Microsoft.WindowsAzure.Storage.Table;
namespace Transfer_data_from_csv.Services
{
    class AnswerDataService
    {
        public static bool InsertData(List<AnswerEntities> entities, string accountName, string accountKey)
        {
            var table = CloudTableService.GetAuthTable(accountName,  accountKey);
            TableBatchOperation tablesBatch = new TableBatchOperation();

            for (int i = 0; i < entities.Count; i++)
            {
                TableOperation insert = TableOperation.Insert(entities[i]);
               
                tablesBatch.Add(insert);
                
            }

            table.ExecuteBatchAsync(tablesBatch);
          

            return true;
        }
    }
}

