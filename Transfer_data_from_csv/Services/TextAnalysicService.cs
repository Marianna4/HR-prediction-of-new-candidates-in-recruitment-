using System;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;
using System.Text;

namespace Transfer_data_from_csv.Services
{
    class TextAnalysicService
    {
        public string KeyPhraseExtractionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.ExtractKeyPhrases(Text);
            string keyPH="";

            foreach (string keyphrase in response.Value.KeyPhrases)
            {
                keyPH = keyPH + keyphrase+", ";
            }
            return keyPH;
        }
        public string LanguageDetectionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client,string Text)
        {
            var response = client.DetectLanguage(Text);
            var detectedLanguage = response.Value.PrimaryLanguage;
            return detectedLanguage.Name;
        }
        public double SentimentAnalysisExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.AnalyzeSentiment(Text);
            var sent = 0.0;           
            foreach (var sentence in response.Value.SentenceSentiments)
            {                
                if (sentence.PositiveScore > sentence.NegativeScore) sent = (sentence.PositiveScore);
                else sent = sentence.NegativeScore * -1;
            }
            return sent;
        }
        public string ModerateText(ContentModeratorClient client, string Text)
        {
            Console.WriteLine("TEXT MODERATION");
            Console.WriteLine();         
            Text= Text.Replace(Environment.NewLine, " ");
            byte[] textBytes = Encoding.UTF8.GetBytes(Text);
            MemoryStream stream = new MemoryStream(textBytes);

                using (client)
                {
                try
                {
                    var screenResult = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
                    return screenResult.ToString();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "";
                }
                }

            

         
        }
    }
}

