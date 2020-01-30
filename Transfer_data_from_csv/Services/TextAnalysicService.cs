using System;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Azure.AI.TextAnalytics;



namespace Transfer_data_from_csv.Services
{
    class TextAnalysicService
    {
        public void KeyPhraseExtractionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.ExtractKeyPhrases(Text);

            // Printing key phrases
            Console.WriteLine("Key phrases:");

            foreach (string keyphrase in response.Value.KeyPhrases)
            {
                Console.WriteLine($"\t{keyphrase}");
            }
        }
        public string languageDetectionExample(ITextAnalyticsClient client, string Text)
        {

            try
            {
                var result = client.DetectLanguage(Text);
                Console.WriteLine($"Language: {result.DetectedLanguages[0].Name}");
                return result.DetectedLanguages[0].Name;
            }
            catch (Exception e)
            {
                var eror = e;
                Console.WriteLine(eror.Message);
                return eror.Message;
            }
        }
        public void SentimentAnalysisExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.AnalyzeSentiment(Text);
            Console.WriteLine($"Document sentiment: {response.Value.DocumentSentiment.SentimentClass}\n");
            foreach (var sentence in response.Value.SentenceSentiments)
            {
                Console.WriteLine($"\tSentence [offset {sentence.Offset}, length {sentence.Length}]");
                Console.WriteLine($"\tSentence sentiment: {sentence.SentimentClass}");
                Console.WriteLine($"\tPositive score: {sentence.PositiveScore:0.00}");
                Console.WriteLine($"\tNegative score: {sentence.NegativeScore:0.00}");
                Console.WriteLine($"\tNeutral score: {sentence.NeutralScore:0.00}\n");
            }
        }
    }
}

