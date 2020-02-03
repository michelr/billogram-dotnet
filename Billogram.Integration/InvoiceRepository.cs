using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using System;
using System.Configuration;
using System.Threading.Tasks;

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

        public async Task<Response<Invoice>> Create(Invoice invoice)
        {
            string result = "";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            var content = JsonConvert.SerializeObject(invoice, dateTimeConverter);
            try
            {
                result = await client.Post("billogram", content);
                var data = JsonConvert.DeserializeObject<Response<Invoice>>(result);
                return data;
            } catch(Exception ex)
            {
                logger.Error(string.Format("Create - {0}: {1}",
                    result,
                    content));
                throw ex;
            }
        }

        public async Task<Response<Invoice>> Send(string invoiceId)
        {
            string result = "";
            try
            {
                result = await client.Post(
                    string.Format("billogram/{0}/command/send", invoiceId), @"{ ""method"": ""Email"" }");
                var data = JsonConvert.DeserializeObject<Response<Invoice>>(result);
                return data;
            }
            catch(Exception ex)
            {
                logger.Error(string.Format("Create - {0}: {1}",
                    result,
                    invoiceId));
                throw ex;
            }
        }


    }
}
