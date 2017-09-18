using Billogram.Integration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Billogram.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void GetCustomer()
        {
            var customer = new CustomerRepository();
            var result = customer.Get(1);
            Assert.Equal("OK", result.Status);
        }

        [Fact]
        public void GetCustomerWithEmailAndBirthDate()
        {
            var customer = new CustomerRepository();
            var result = customer.Get("mr@michel.se", "790118");
            Assert.Equal("OK", result.Status);
            Assert.True(result.Data.Any());
        }

        [Fact]
        public void CreateCustomer()
        {
            var customer = new CustomerRepository();
            var response = customer.Create(new Customer {
                Name = "Michel R",
                Type = "individual",
                ContactInfo = new ContactInfo
                {
                    Email = "mr@michel.se",
                    Phone = "0709-757383"
                },
                Address = new Address
                {
                    Street = "Gotlandsgatan 60",
                    Zipcode = "11665",
                    City = "Sthlm",
                    Country = "SE"
                }

            });

            Assert.Equal("OK", response.Status);
        }
    }
}
