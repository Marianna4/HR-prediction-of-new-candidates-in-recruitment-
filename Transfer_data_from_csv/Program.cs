using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.IO;
namespace Transfer_data_from_csv
{
    class Program
    {
        //Connect to Account & choose table
        private static CloudTable AuthTable()
        {
            string accountName = "internsq42019";
            string accountKey = "wNEBN4s0BnC+IcrCtON2o5R1DDkgJre1oDMxjCSGao9+hgMsx7rgA4SNd7f7Uypo6VdqCqRYsR/AhyjfAS6reQ==";
            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudTableClient client = account.CreateCloudTableClient();

                CloudTable table = client.GetTableReference("PreliminaryData");
               // Console.WriteLine("SUCCESS");
                return table;
          
            }
            catch
            {
                return null;
            }
        }

        // Create new Entity
        public class AnsEntity : TableEntity
        {
            public AnsEntity() { }

            public string Name { get; set; }
            public string Email { get; set; }
            public string Answer { get; set; }
            public bool IsProcessed { get; set; }

        }
        private static bool CreateEntity(int i,List<string> listPK, List<string> listRK, List<string> listName, List<string> listEmail, List<string> listAnswer, CloudTable table)
        {        
               var newEntity = new AnsEntity()
                {
                    PartitionKey = listPK[i],
                    RowKey = listRK[i],
                    Name = listName[i],
                    Email = listEmail[i],
                    Answer = listAnswer[i],
                    IsProcessed = false
                };

            TableOperation insert = TableOperation.Insert(newEntity);
            
                try
                {

                    table.ExecuteAsync(insert);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }         
           
            
        }
      //Insert Data into Azure Table
        public static string InsertData(List<string>listPK, List<string> listRK, List<string> listName, List<string> listEmail, List<string> listAnswer)
        {
            var table = AuthTable();
            for (int i = 1; i < listRK.Count; i++)
            {
                var success = CreateEntity(i,listPK, listRK, listName, listEmail, listAnswer, table);

                if (!success)
                {
                    return "Error";
                }
               

            }
            return "EndWriting";
        }
        static void Main(string[] args)
        {
            //string csv_file_path = @"D:\Диплом\LocalRepo\Answers.csv";
            List<string> listPK = new List<string>();
            List<string> listRK = new List<string>();
            List<string> listName = new List<string>();
            List<string> listEmail = new List<string>();
            List<string> listAnswer = new List<string>();
            using (var reader = new StreamReader(@".\Answers5.csv"))
            {
               

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listPK.Add(values[0]);
                    listRK.Add(values[1]);
                    listName.Add(values[2]);
                    listEmail.Add(values[3]);
                    listAnswer.Add(values[4]);

                }
               
            }

            InsertData(listPK, listRK, listName, listEmail, listAnswer);

            Console.WriteLine("Press any key");
                Console.ReadKey();
        }

    }
    
}
