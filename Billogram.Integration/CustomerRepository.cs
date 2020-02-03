using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

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

        public async Task<Response<Customer>> Create(Customer customer)
        {
            var content = JsonConvert.SerializeObject(customer);
            string result = "";
            try
            {
                result = await client.Post("customer", content);
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<Customer>>(result, dateTimeConverter);
                return data;
            } catch(Exception ex)
            {
                logger.Error(ex, "Create:", result, content);
                throw ex;
            }
        }

        public async Task<Response<Customer>> Get(int customerNo)
        {
            string result = "";
            try
            {
                result = await client.Get(string.Format("customer/{0}", customerNo));
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<Customer>>(result, dateTimeConverter);
                return data;
            }catch (Exception ex)
            {
                logger.Error(ex, "Get with customerNo:", result, customerNo);
                throw ex;
            }
        }

        public async Task<Response<List<Customer>>> Get(string email, string birthDate)
        {
            var url = string.Format("customer?filter_field=contact:email&filter_value={0}&page=1&page_size=10&filter_type=field&filter_field=org_no&filter_value={1}", email, birthDate);

            string result = "";
            try
            {
                result = await client.Get(url);
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var data = JsonConvert.DeserializeObject<Response<List<Customer>>>(result, dateTimeConverter);
                return data;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Get with email and birthDate:", 
                    result,
                    email,
                    birthDate);
                throw ex;
            }
        }
    }
}
