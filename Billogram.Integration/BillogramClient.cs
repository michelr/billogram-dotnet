using System;
using System.Net.Http;
using System.Text;

namespace Billogram.Integration
{
    class BillogramClient
    {
        HttpClient client;

        public BillogramClient(string baseAddress, string userId, string password)
        {
            client = new HttpClient{ BaseAddress = new Uri(baseAddress) };
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userId, password));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public HttpResponseMessage Post(string requestUri, string content)
        {
            var postData = new StringContent(content, Encoding.UTF8, "application/json");
            var result = client.PostAsync(requestUri, postData).Result;
            return result;
        }

        public HttpResponseMessage Get(string requestUri)
        {
            var result = client.GetAsync(requestUri).Result;
            return result;
        }
    }
}
