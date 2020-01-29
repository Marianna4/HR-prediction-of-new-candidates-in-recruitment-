using System;
using System.Collections.Generic;
using Transfer_data_from_csv.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
namespace Transfer_data_from_csv.Services
{
    class AnswerDataService
    {
        public async Task<bool> InsertData(List<AnswerEntities> entities, string accountName, string accountKey)
        {
            var tablesBatch = new TableBatchOperation();
            var tableService = new CloudTableService();
            var table = tableService.GetAuthTable(accountName, accountKey);

            for (int i = 0; i < entities.Count; i++)
            {
                var insert = TableOperation.InsertOrReplace(entities[i]);

                tablesBatch.Add(insert);
            }


            await table.ExecuteBatchAsync(tablesBatch);


            return true;
        }
    }
}




