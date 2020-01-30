using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Transfer_data_from_csv.Entities
{

    public class AnalyzedDatasEntities : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsProcessed { get; set; }
        public string Q1Reply { get; set; }
        public int Q1Lenghth { get; set; }
        public string Q1Language { get; set; }
        public string Q1KeyPhrases { get; set; }
        public double Q1Sentiment { get; set; }
        public string Q1ProfanityTerms { get; set; }
    }
    

}
