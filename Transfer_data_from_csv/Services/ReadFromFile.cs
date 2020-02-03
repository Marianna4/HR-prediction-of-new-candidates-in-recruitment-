using System.IO;
using Transfer_data_from_csv.Entities;
using System.Reflection;
using System.Collections.Generic;
namespace Transfer_data_from_csv.Services
{
    class ReadFromFile
    {
       public List<AnswerEntities> FileReading()
        {           
            var entities = new List<AnswerEntities>();
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Answers5.csv");
            using (var reader = new StreamReader(@"Answers5.csv"))
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
                        Answer1 = values[4],
                        Answer2 = values[5],
                        Answer3 = values[6],
                        Answer4 = values[7],
                        IsProcessed = false
                    };

                    entities.Add(newEntity);
                }
            }
            return entities;
        }
    }
}
