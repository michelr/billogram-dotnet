using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Billogram.Integration
{
    class BillogramClient
    {
        HttpClient client;

        public BillogramClient(string baseAddress, string userId, string password)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            client = new HttpClient{ BaseAddress = new Uri(baseAddress) };
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userId, password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Post(string requestUri, string content)
        {
            var postData = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, postData);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<string> Get(string requestUri)
        {
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
