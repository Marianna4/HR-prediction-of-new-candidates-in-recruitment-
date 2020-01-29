using System;
using System.Collections.Generic;
using System.IO;
using Transfer_data_from_csv.Services;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
using System.Reflection;
using System.Threading.Tasks;
namespace Transfer_data_from_csv
{
    class Program
    {
        static async Task Main(string[] args)
        {         
            var entities = new List<AnswerEntities>();
            using (var reader = new StreamReader(@"D:\Diploma\LocalRepo\Answers5.csv"))
            {
                if (!reader.EndOfStream)
                {
                    reader.ReadLine();
                }
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var newEntity = new AnswerEntities()
                    {
                        PartitionKey = values[0],
                        RowKey = values[1],
                        Name = values[2],
                        Email = values[3],
                        Answer = values[4],
                        IsProcessed = false
                    };

                    entities.Add(newEntity);
                 
                }

            }                                 
            var dataService = new AnswerDataService();
            try
            {
                await dataService.InsertData(entities, ConstantHelper.accountName, ConstantHelper.accountKey);
            }
            catch (Exception ex)
            {
                var v = ex;
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

    }

}

