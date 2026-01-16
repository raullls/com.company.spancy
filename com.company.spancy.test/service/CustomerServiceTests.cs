using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class CustomerServiceTests : BaseTest
    {
        public ICustomerService CustomerService { get; set; }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.CustomerService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            CustomerDto customerDto1 = new CustomerDto();
            customerDto1.Name = "Alex";
            customerDto1.Amount = 200;
            this.CustomerService.CreateCustomerTX(customerDto1);

            CustomerDto customerDto2 = new CustomerDto();
            customerDto2.Name = "Fred";
            customerDto2.Amount = 0;
            Response data = this.CustomerService.CreateCustomerTX(customerDto2) as dynamic;
            Assert.IsTrue(data.Errors.Count > 0);

            Assert.AreEqual(1, this.CustomerService.FindAllRO().Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            CustomerDto customerDto1 = new CustomerDto();
            customerDto1.Name = "Alex";
            customerDto1.Amount = 200;
            this.CustomerService.CreateCustomerTX(customerDto1);

            CustomerDto customerDto = this.CustomerService.FindAllRO().First();

            CustomerDto customerDto2 = this.CustomerService.FindByIdRO(customerDto.Id);
            Assert.AreEqual("Alex", customerDto2.Name);
        }

        [TestMethod]
        public void RemoveTest()
        {
            CustomerDto customerDto1 = new CustomerDto();
            customerDto1.Name = "Alex";
            customerDto1.Amount = 200;
            this.CustomerService.CreateCustomerTX(customerDto1);

            CustomerDto customerDto2 = new CustomerDto();
            customerDto2.Name = "Fred";
            customerDto2.Amount = 400;
            this.CustomerService.CreateCustomerTX(customerDto2);

            IList<CustomerDto> customerDtos = this.CustomerService.FindAllRO();
            Assert.AreEqual(2, customerDtos.Count);

            this.CustomerService.RemoveByIdTX(customerDtos.First().Id);
            Assert.AreEqual(1, this.CustomerService.FindAllRO().Count);
        }
    }
}
