using System;
using System.Collections.Generic;
using Transfer_Data_To_Cognitive_Service.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;


namespace Transfer_Data_To_Cognitive_Service.Services
{
    class DataFromTableService
    {
        public async Task<bool> OutputData(List<EntitiesFromTable> DataList, string accountName, string accountKey)
        {
            var tableService = new CloudTableService();
            var table = tableService.GetAuthTable(accountName, accountKey);           
            var condition = TableQuery.GenerateFilterConditionForBool("IsProcessed", QueryComparisons.Equal, false);
            var query = new TableQuery<EntitiesFromTable>().Where(condition);
            TableContinuationToken token = null;
            do
            {               
                var segment = await table.ExecuteQuerySegmentedAsync(query, token);
                foreach (EntitiesFromTable entity in segment)
                {                    
                    DataList.Add(entity);
                }                
                token = segment.ContinuationToken;
            }
            while (token != null);       

            return true;
        }

    }
}





