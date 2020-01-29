using System;
using System.Collections.Generic;
using Transfer_Data_To_Cognitive_Service.Services;
using Transfer_Data_To_Cognitive_Service.Entities;
using Transfer_Data_To_Cognitive_Service.Helpers;
using System.Threading.Tasks;

namespace Transfer_Data_To_Cognitive_Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var entities = new List<EntitiesFromTable>();
            var tableData = new DataFromTableService();

            try
            {
                await tableData.OutputData(entities, ConstantHelper.accountName, ConstantHelper.accountKey);
            }
            catch (Exception ex)
            {
                var v = ex;
            }
            //foreach (EntitiesFromTable entity in entities)
            //{ 
            //Console.WriteLine("{0}, {1}\t{2}\t{3}\t{4}", entity.PartitionKey, entity.RowKey,
            //                        entity.Name, entity.Answer, entity.Email);
            //}
            Console.WriteLine("Press any key");
            Console.ReadKey();

        }
    }
}
