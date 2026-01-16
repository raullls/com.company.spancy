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
    public class LandTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Estate p1 = new Estate();
            p1.Name = "Estate";
            this.SessionFactory.GetCurrentSession().Save(p1);

            Land land = new Land();
            land.Name = "Land";
            land.Area = 20;
            this.SessionFactory.GetCurrentSession().Save(land);

            land.Name = "Land Estate";
            land.Area = 30;
            this.SessionFactory.GetCurrentSession().Merge(land);

            IList<Land> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Land").List<Land>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(land);

            IList<Land> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Land").List<Land>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
