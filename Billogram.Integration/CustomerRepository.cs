using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using System.Collections.Generic;
using System.Configuration;

namespace Billogram.Integration
{
    public class CustomerRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BillogramClient client;

        public CustomerRepository()
        {
            var url = ConfigurationManager.AppSettings["billogramApiServiceUrl"];
            var userId = ConfigurationManager.AppSettings["billogramApiUserId"];
            var password = ConfigurationManager.AppSettings["billogramApiPassword"];
            client = new BillogramClient(url, userId, password);
        }

        public Response<Customer> Create(Customer customer)
        {
            var content = JsonConvert.SerializeObject(customer);
            var postResult = client.Post("customer", content);
            if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = postResult.Content.ReadAsStringAsync().Result;
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<Customer>>(result, dateTimeConverter);
                return data;
            } else
            {
                logger.Error(string.Format("Create - {0}: {1}",
                    postResult.StatusCode,
                    content));
                return new Response<Customer> { Status = postResult.StatusCode.ToString() };
            }
        }

        public Response<Customer> Get(int customerNo)
        {
            var postResult = client.Get(string.Format("customer/{0}", customerNo));
            if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = postResult.Content.ReadAsStringAsync().Result;
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<Customer>>(result, dateTimeConverter);
                return data;
            }
            else
            {
                logger.Error(string.Format("Get - {0}: {1}",
                    postResult.StatusCode,
                    customerNo));
                return new Response<Customer> { Status = postResult.StatusCode.ToString() };
            }
        }

        public Response<List<Customer>> Get(string email, string birthDate)
        {
            var url = string.Format("customer?filter_field=contact:email&filter_value={0}&page=1&page_size=10&filter_type=field&filter_field=org_no&filter_value={1}", email, birthDate);
            var postResult = client.Get(url);
            if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = postResult.Content.ReadAsStringAsync().Result;
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<List<Customer>>>(result, dateTimeConverter);
                return data;
            }
            else
            {
                logger.Error(string.Format("Get - {0}: {1} {2}",
                    postResult.StatusCode,
                    email,
                    birthDate));
                return new Response<List<Customer>> { Status = postResult.StatusCode.ToString() };
            }
        }
    }
}
