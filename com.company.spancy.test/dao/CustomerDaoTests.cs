using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class CustomerDaoTests : BaseTest
    {
        public ICustomerDao CustomerDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.CustomerDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex";
            Cart cart1 = new Cart();
            cart1.Amount = 300.0;
            customer1.Cart = cart1;
            this.CustomerDao.SaveEntity(customer1);

            Customer customer2 = new Customer();
            customer2.Name = "Fred";
            Cart cart2 = new Cart();
            cart2.Amount = 500.0;
            customer2.Cart = cart2;
            this.CustomerDao.SaveEntity(customer2);

            Assert.AreEqual(2, this.CustomerDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex";
            Cart cart1 = new Cart();
            cart1.Amount = 300.0;
            customer1.Cart = cart1;
            this.CustomerDao.SaveEntity(customer1);

            IList<Customer> customers = this.CustomerDao.LoadAllEntities();
            Customer tempCustomer = customers[0];
            Customer cust = this.CustomerDao.LoadEntity(tempCustomer.Id);
            Assert.AreEqual(tempCustomer.Name, cust.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex";
            Cart cart1 = new Cart();
            cart1.Amount = 300.0;
            customer1.Cart = cart1;
            this.CustomerDao.SaveEntity(customer1);

            Customer customer2 = new Customer();
            customer2.Name = "Fred";
            Cart cart2 = new Cart();
            cart2.Amount = 500.0;
            customer2.Cart = cart2;
            this.CustomerDao.SaveEntity(customer2);

            Assert.AreEqual(2, this.CustomerDao.LoadAllEntities().Count);

            this.CustomerDao.DeleteEntity(customer2);
            Assert.AreEqual(1, this.CustomerDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex";
            Cart cart1 = new Cart();
            cart1.Amount = 300.0;
            customer1.Cart = cart1;
            this.CustomerDao.SaveEntity(customer1);
            Assert.AreEqual(1, this.CustomerDao.LoadAllEntities().Count);

            IList<Customer> customers = this.CustomerDao.LoadAllEntities();
            Customer customer2 = customers[0];
            customer2.Name = "Fred";
            customer2.Cart.Amount = 500.0;
            this.CustomerDao.UpdateEntity(customer2);

            IList<Customer> customers1 = this.CustomerDao.LoadAllEntities();
            Customer customer3 = customers1[0];
            Assert.AreEqual("Fred", customer3.Name);
            Assert.AreEqual(500.0, customer3.Cart.Amount);
        }
    }
}
