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
    public class InvoiceTests
    {
        [Fact]
        public void CreateInvoice()
        {
            var customer = new CustomerRepository();
            var c = customer.Get(50).GetAwaiter().GetResult();
            var invoice = new InvoiceRepository();
            var response = invoice.Create(new Invoice
            {
                Customer = new Customer { CustomerNo = c.Data.CustomerNo },
                Items = new Item[] {
                    new Item { Id = "Praia-01", Count = 5, Discount = 50 },
                    new Item { Id = "Praia-23", Count = 1 }
                },
                Information = new Info { OrderNo = 1, OrderDate = DateTime.Today, Message = "Ett fakturameddeande. Betala!!" },
                DueDate = DateTime.Now.AddDays(3),
                InterestRate = 0,
                ReminderFee = 0,
                AutoReminders = true,
                AutoRemindersSettings = new Reminder[] { new Reminder { DelayDays = 3, Message = "Glöm inte betala" } }
            }).GetAwaiter().GetResult();
            Assert.Equal("OK", response.Status);
        }

        [Fact]
        public void SendInvoice()
        {
            var invoice = new InvoiceRepository();
            var response = invoice.Send("vt2nWyp").GetAwaiter().GetResult();
            Assert.Equal("OK", response.Status);
        }
    }
}
