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
    public class TCPTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Protocol p1 = new Protocol();
            p1.Name = "Protocol";

            this.SessionFactory.GetCurrentSession().Save(p1);

            Tcp tcp = new Tcp();
            tcp.Name = "TCP";

            this.SessionFactory.GetCurrentSession().Save(tcp);

            tcp.Name = "TCP/IP";

            this.SessionFactory.GetCurrentSession().Merge(tcp);

            IList<Tcp> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Tcp").List<Tcp>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(tcp);

            IList<Tcp> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Tcp").List<Tcp>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
