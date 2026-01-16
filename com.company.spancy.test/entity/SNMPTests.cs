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
    public class SNMPTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Protocol p1 = new Protocol();
            p1.Name = "Protocol";

            this.SessionFactory.GetCurrentSession().Save(p1);

            Snmp snmp = new Snmp();
            snmp.Name = "SNMP";

            this.SessionFactory.GetCurrentSession().Save(snmp);

            snmp.Name = "SNMP Protocol";

            this.SessionFactory.GetCurrentSession().Merge(snmp);

            IList<Snmp> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Snmp").List<Snmp>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(snmp);

            IList<Snmp> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Snmp").List<Snmp>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
