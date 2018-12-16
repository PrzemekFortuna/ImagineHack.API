using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizITAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuizITAPI.Services
{
    public class OCRService
    {
        private const string _subscriptionKey = "b5b4b04630d647219f5fefd087a44716";
        private const string _baseUri = @"https://westeurope.api.cognitive.microsoft.com/vision/v2.0/ocr/";

        public async Task<string> MakeOCRRequest(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", _subscriptionKey);

                string requestParameters = "language=unk&detectOrientation=true/";

                string uri = _baseUri;

                HttpResponseMessage response;

                byte[] byteData = GetImageAsByteArray(url);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    response = await client.PostAsync(uri, content);
                }

                // Asynchronously get the JSON response.
                string contentJson = await response.Content.ReadAsStringAsync();
                var contentString = string.Empty;
                JsonConvert.DeserializeObject<RootObject>(contentJson).regions.ForEach(t => t.lines.ForEach(c => c.words.ForEach(d => contentString += d.text + " ")));
                return contentString;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private byte[] GetImageAsByteArray(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(url);
                    return imageBytes;
                }
                catch(Exception)
                {
                    return null;
                }
            }            
        }
    }
}
