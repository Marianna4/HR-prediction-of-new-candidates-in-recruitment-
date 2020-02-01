using System;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Transfer_data_from_csv.Helpers;

namespace Transfer_data_from_csv.Services
{
    class TextAnalysicService
    {
        public string KeyPhraseExtractionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
        {
            var response = client.ExtractKeyPhrases(Text);
            string keyPH = "";

            foreach (string keyphrase in response.Value.KeyPhrases)
            {
                keyPH = keyPH + keyphrase + ", ";
            }
            return keyPH;
        }
        public string LanguageDetectionExample(Azure.AI.TextAnalytics.TextAnalyticsClient client, string Text)
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
            Console.WriteLine();
            Text = Text.Replace(Environment.NewLine, " ");
            byte[] textBytes = Encoding.UTF8.GetBytes(Text);
            MemoryStream stream = new MemoryStream(textBytes);

            using (client)
            {
                try
                {
                    ContentModeratorClient clientText = new ContentModeratorClient(new ApiKeyServiceClientCredentials(ConstantHelper.ApiKey));
                    clientText.Endpoint = ConstantHelper.endpoint.OriginalString;

                    var screenResult = clientText.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
                   
                   var templ= JsonConvert.SerializeObject(screenResult, Newtonsoft.Json.Formatting.Indented);
                    
                    templ = templ.ToString();
                    int wordLen=7;                    
                   int start = templ.IndexOf("Terms")+ wordLen;
                   int end = templ.IndexOf("TrackingId");
                    string Terms=templ.Substring(start,end-start-3);
                    return Terms;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

        }
    }
}

