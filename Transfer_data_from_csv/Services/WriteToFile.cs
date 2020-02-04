using System.IO;
using Transfer_data_from_csv.Entities;
using System.Reflection;
using System.Collections.Generic;
using CsvHelper;

namespace Transfer_data_from_csv.Services
{
    class WriteToFile
    {
        public void WriteToCSV(List<AnalyzedDatasEntities> dataFromTable)
        {

            Directory.SetCurrentDirectory(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "../../.."));
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"AnalizedData.csv");
            using (var writer = new StreamWriter(@"AnalizedData.csv"))
            {               
                foreach (var temp in dataFromTable)
                {
                    var newLine = string.Format("{0};{1};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26};{27};{28};{29};", temp.PartitionKey, temp.RowKey,temp.Name,temp.Email,temp.IsProcessed,temp.Q1Reply,temp.Q1Lenghth,temp.Q1Language,temp.Q1KeyPhrases,temp.Q1Sentiment,temp.Q1ProfanityTerms, temp.Q2Reply, temp.Q2Lenghth, temp.Q2Language, temp.Q2KeyPhrases, temp.Q2Sentiment, temp.Q2ProfanityTerms, temp.Q3Reply, temp.Q3Lenghth, temp.Q3Language, temp.Q3KeyPhrases, temp.Q3Sentiment, temp.Q3ProfanityTerms, temp.Q4Reply, temp.Q4Lenghth, temp.Q4Language, temp.Q4KeyPhrases, temp.Q4Sentiment, temp.Q4ProfanityTerms,temp.ManuallyEvalutate);
                   writer.WriteLine(newLine);
                    writer.Flush();
                }
            
            }
        }
    }
}
