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
    public class ProtocolTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Protocol p1 = new Protocol();
            p1.Name = "A";

            Protocol p2 = new Protocol();
            p2.Name = "B";

            this.SessionFactory.GetCurrentSession().Save(p1);
            this.SessionFactory.GetCurrentSession().Save(p2);
            
            p1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(p1);

            IList<Protocol> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Protocol").List<Protocol>();
            Assert.AreEqual(2L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(p1);

            IList<Protocol> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Protocol").List<Protocol>();
            Assert.AreEqual(1L, list2.Count());
        }
    }
}
