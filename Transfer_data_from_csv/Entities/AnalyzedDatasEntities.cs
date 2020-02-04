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
        public string Q2Reply { get; set; }
        public int Q2Lenghth { get; set; }
        public string Q2Language { get; set; }
        public string Q2KeyPhrases { get; set; }
        public double Q2Sentiment { get; set; }
        public string Q2ProfanityTerms { get; set; }
        public string Q3Reply { get; set; }
        public int Q3Lenghth { get; set; }
        public string Q3Language { get; set; }
        public string Q3KeyPhrases { get; set; }
        public double Q3Sentiment { get; set; }
        public string Q3ProfanityTerms { get; set; }
        public string Q4Reply { get; set; }
        public int Q4Lenghth { get; set; }
        public string Q4Language { get; set; }
        public string Q4KeyPhrases { get; set; }
        public double Q4Sentiment { get; set; }
        public string Q4ProfanityTerms { get; set; }
        public int ManuallyEvalutate { get; set; }

    } 

}
