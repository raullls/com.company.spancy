using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class ProductTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Product p1 = new Product();
            p1.Name = "A";

            Product p2 = new Product();
            p2.Name = "B";

            this.SessionFactory.GetCurrentSession().Save(p1);
            this.SessionFactory.GetCurrentSession().Save(p2);

            p1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(p1);

            IList<Product> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Product").List<Product>();
            Assert.AreEqual(2, list.Count);

            this.SessionFactory.GetCurrentSession().Delete(p1);
            IList<Product> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Product").List<Product>();
            Assert.AreEqual(1, list2.Count);
        }
    }
}
