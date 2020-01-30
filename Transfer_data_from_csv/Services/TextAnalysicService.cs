using System;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Azure.AI.TextAnalytics;



namespace Transfer_data_from_csv.Services
{
    class TextAnalysicService
    {
        public string KeyPhraseExtractionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.ExtractKeyPhrases(Text);
            string keyPH="";
            // Printing key phrases
            //Console.WriteLine("Key phrases:");

            foreach (string keyphrase in response.Value.KeyPhrases)
            {
                //Console.WriteLine($"\t{keyphrase}");
                keyPH = keyPH + keyphrase+", ";
            }
            return keyPH;
        }
        public string LanguageDetectionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client,string Text)
        {
            var response = client.DetectLanguage(Text);
            var detectedLanguage = response.Value.PrimaryLanguage;
           // Console.WriteLine("Language:");
           // Console.WriteLine($"\t{detectedLanguage.Name},\tISO-6391: {detectedLanguage.Iso6391Name}\n");

            return detectedLanguage.Name;
        }
        public double SentimentAnalysisExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.AnalyzeSentiment(Text);
            var sent = 0.0;
           // Console.WriteLine($"Document sentiment: {response.Value.DocumentSentiment.SentimentClass}\n");
            foreach (var sentence in response.Value.SentenceSentiments)
            {
                //Console.WriteLine($"\tSentence [offset {sentence.Offset}, length {sentence.Length}]");
                //Console.WriteLine($"\tSentence sentiment: {sentence.SentimentClass}");
                //Console.WriteLine($"\tPositive score: {sentence.PositiveScore:0.00}");
                //Console.WriteLine($"\tNegative score: {sentence.NegativeScore:0.00}");
                //Console.WriteLine($"\tNeutral score: {sentence.NeutralScore:0.00}\n");
                if (sentence.PositiveScore > sentence.NegativeScore) sent = (sentence.PositiveScore);
                else sent = sentence.NegativeScore * -1;
            }
            return sent;
        }
    }
}

