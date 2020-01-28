using System;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.IO;
using Transfer_data_from_csv.Services;
using Transfer_data_from_csv.Entities;
using Transfer_data_from_csv.Helpers;
namespace Transfer_data_from_csv
{
    class Program
    {
       

     
        static void Main(string[] args)
        {
            

            var entities = new List<AnswerEntities>();

            using (var reader = new StreamReader(@"D:\Diploma\LocalRepo\Answers5.csv"))
            {
                if (!reader.EndOfStream)
                {
                    reader.ReadLine();
                }
                int i = 0;
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

           

            AnswerDataService.InsertData(entities,ConstantHelper.accountName,ConstantHelper.accountKey);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

    }

}

