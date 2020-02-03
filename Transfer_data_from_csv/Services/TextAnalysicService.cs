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
                if (sentence.SentimentClass.ToString()== "Positive") sent = sentence.PositiveScore;
                if (sentence.SentimentClass.ToString() == "Neutural") sent =0.0;
                if (sentence.SentimentClass.ToString() == "Negative") sent = 1 - sentence.NegativeScore;
                else sent = 0.5;
            }
            return sent;
        }
        public string ModerateText(ContentModeratorClient client, string Text)
        {  
            Text = Text.Replace(Environment.NewLine, " ");
            byte[] textBytes = Encoding.UTF8.GetBytes(Text);
            MemoryStream stream = new MemoryStream(textBytes);

            using (client)
            {
                try
                {
                    ContentModeratorClient clientText = new ContentModeratorClient(new ApiKeyServiceClientCredentials(ConstantHelper.ApiKey));
                    clientText.Endpoint = ConstantHelper.endpoint.OriginalString;

                    var screenResult = clientText.TextModeration.ScreenText("text/plain", stream, "eng", false, false, null, true);
                   
                   var templ= JsonConvert.SerializeObject(screenResult.Terms, Newtonsoft.Json.Formatting.Indented);
                  
                    return templ.ToString();
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

        }
    }
}

