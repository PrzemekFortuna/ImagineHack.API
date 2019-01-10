using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizITAPI.Helpers;
using QuizITAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace QuizITAPI.Services
{
    public class OCRService : IOCRService
    {
        private const string _subscriptionKey = "b5b4b04630d647219f5fefd087a44716";
        private const string _baseUri = @"https://westeurope.api.cognitive.microsoft.com/vision/v2.0/";

        public async Task<string> MakeOCRRequest(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", _subscriptionKey);

                string uri = _baseUri + "ocr/";

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
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> MakeOCRImageRequestAsync(IFormFile image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.CopyTo(memoryStream);

            var imgArray = memoryStream.ToArray();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c70d90efc56d489c8ff1e3b15d74539f");

            var queryParameter = HttpUtility.ParseQueryString(String.Empty);
            queryParameter["mode"] = "Printed";
            string uri = _baseUri + @"recognizeText?" + queryParameter;

            var response = await GetInformationFromOCRService(imgArray, client, uri);

            string contentString = null;
            if (response.IsSuccessStatusCode)
            {
                contentString = await ExtractTextRecognition(client, response, contentString);
            }

            return contentString;
        }
        private async Task<string> ExtractTextRecognition(HttpClient client, HttpResponseMessage response, string contentString)
        {
            var operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            HttpResponseMessage textRecognitionResponse = null;
            int i = 0;
            do
            {
                System.Threading.Thread.Sleep(1000);
                textRecognitionResponse = await client.GetAsync(operationLocation);
                contentString = await textRecognitionResponse.Content.ReadAsStringAsync();
            }
            while (++i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);
            return contentString;
        }

        private async Task<HttpResponseMessage> GetInformationFromOCRService(byte[] imgArray, HttpClient client, string uri)
        {
            HttpResponseMessage response;
            using (ByteArrayContent content = new ByteArrayContent(imgArray))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);
            }
            return response;
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
