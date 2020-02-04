using System;
using System.Collections.Generic;
using Transfer_data_from_csv.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using Transfer_data_from_csv.Helpers;



namespace Transfer_data_from_csv.Services
{
    class DataFromTableService
    {
        public async Task<List<T>> OutputData<T>( string accountName, string accountKey,string tableName)where T:TableEntity, new()
        {
            
            var DataList = new List<T>();
            var tableService = new CloudTableService();
            var table = tableService.GetAuthTable(accountName, accountKey, tableName);
            var condition = "";
            if (tableName == "PreliminaryData")
            {
                condition = TableQuery.GenerateFilterConditionForBool("IsProcessed", QueryComparisons.Equal, false);
            }
            else
            {
                condition = TableQuery.GenerateFilterConditionForInt("ManuallyEvalutate", QueryComparisons.Equal, 0);
            }
            var query = new TableQuery<T>().Where(condition);
            TableContinuationToken token = null;
            do
            {
                var segment = await table.ExecuteQuerySegmentedAsync(query, token);
                foreach (T entity in segment)
                {
                    DataList.Add(entity);
                }
                token = segment.ContinuationToken;
            }
            while (token != null);

            return DataList;
        }

    }
}
