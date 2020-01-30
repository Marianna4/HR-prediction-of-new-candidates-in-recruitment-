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
        public async Task<List<AnswerEntities>> OutputData( string accountName, string accountKey)
        {
            var DataList = new List<AnswerEntities>();
            var tableService = new CloudTableService();
            var table = tableService.GetAuthTable(accountName, accountKey, ConstantHelper.firstTrableName);
            var condition = TableQuery.GenerateFilterConditionForBool("IsProcessed", QueryComparisons.Equal, false);
            var query = new TableQuery<AnswerEntities>().Where(condition);
            TableContinuationToken token = null;
            do
            {
                var segment = await table.ExecuteQuerySegmentedAsync(query, token);
                foreach (AnswerEntities entity in segment)
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
