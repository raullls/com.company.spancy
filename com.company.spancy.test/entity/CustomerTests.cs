using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class CustomerTests : BaseTest
    {
        [TestMethod]
        public void testCRUD()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex";
            Cart cart1 = new Cart();
            cart1.Amount = 500.0;
            customer1.Cart = cart1;
            this.SessionFactory.GetCurrentSession().Save(customer1);

            Customer customer2 = new Customer();
            customer2.Name = "Fred";
            Cart cart2 = new Cart();
            cart2.Amount = 700.0;
            customer2.Cart = cart2;
            this.SessionFactory.GetCurrentSession().Save(customer2);

            customer1.Name = "Alex";
            cart1 = customer1.Cart;
            cart1.Amount = 500.0;
            customer1.Cart = cart1;
            this.SessionFactory.GetCurrentSession().Merge(customer1);

            IList<Customer> customers = this.SessionFactory.GetCurrentSession().CreateQuery("select customer from Customer customer order by customer.Id desc").List<Customer>();
            Assert.AreEqual(2, customers.Count);

            this.SessionFactory.GetCurrentSession().Delete(customer2);

            customers = this.SessionFactory.GetCurrentSession().CreateQuery("select customer from Customer customer order by customer.Id desc").List<Customer>();
            Assert.AreEqual(1, customers.Count);
        }
    }
}
