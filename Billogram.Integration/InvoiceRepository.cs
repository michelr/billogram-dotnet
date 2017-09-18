using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using System.Configuration;

namespace Billogram.Integration
{
    public class InvoiceRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BillogramClient client;

        public InvoiceRepository()
        {
            var url = ConfigurationManager.AppSettings["billogramApiServiceUrl"];
            var userId = ConfigurationManager.AppSettings["billogramApiUserId"];
            var password = ConfigurationManager.AppSettings["billogramApiPassword"];
            client = new BillogramClient(url, userId, password);
        }

        public Response<Invoice> Create(Invoice invoice)
        {
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            var content = JsonConvert.SerializeObject(invoice, dateTimeConverter);
            var postResult = client.Post("billogram", content);
            if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = postResult.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Response<Invoice>>(result);
                return data;
            } else
            {
                logger.Error(string.Format("Create - {0}: {1}",
                    postResult.StatusCode,
                    content));
                return new Response<Invoice> { Status = postResult.StatusCode.ToString() };
            }
        }

        public Response<Invoice> Send(string invoiceId)
        {
            var postResult = client.Post(
                string.Format("billogram/{0}/command/send", invoiceId),
                @"{ ""method"": ""Email"" }");
            if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = postResult.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Response<Invoice>>(result);
                return data;
            }
            else
            {
                logger.Error(string.Format("Create - {0}: {1}",
                    postResult.StatusCode,
                    invoiceId));
                return new Response<Invoice> { Status = postResult.StatusCode.ToString() };
            }
        }


    }
}
