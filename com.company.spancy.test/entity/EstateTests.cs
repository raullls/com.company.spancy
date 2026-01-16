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
    public class EstateTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Estate p1 = new Estate();
            p1.Name = "A";

            Estate p2 = new Estate();
            p2.Name = "B";

            this.SessionFactory.GetCurrentSession().Save(p1);
            this.SessionFactory.GetCurrentSession().Save(p2);

            p1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(p1);

            IList<Estate> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Estate").List<Estate>();
            Assert.AreEqual(2L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(p1);

            IList<Estate> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Estate").List<Estate>();
            Assert.AreEqual(1L, list2.Count());
        }
    }
}
