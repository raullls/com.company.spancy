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
    public class ClientTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Client client1 = new Client();
            client1.Name = "Amritendu De";
            Client client2 = new Client();
            client2.Name = "Lalit Narayan Mishra";
            
            this.SessionFactory.GetCurrentSession().Save(client1);
            this.SessionFactory.GetCurrentSession().Save(client2);

            client1.Name = "Hazekul Alam";
            this.SessionFactory.GetCurrentSession().Merge(client1);
            
            IList<Client> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Client").List<Client>();
            Assert.AreEqual(2, list.Count());
            
            this.SessionFactory.GetCurrentSession().Delete(client1);
            IList<Client> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Client").List<Client>();
            Assert.AreEqual(1, list2.Count());

            Account account1 = new Account();
            account1.Number = "Account 1";
            Account account2 = new Account();
            account2.Number = "Account 2";
            this.SessionFactory.GetCurrentSession().Save(account1);
            this.SessionFactory.GetCurrentSession().Save(account2);

            Client client3 = list2[0];
            client3.Accounts.Add(account1);
            client3.Accounts.Add(account2);
            this.SessionFactory.GetCurrentSession().Merge(client3);

            IList<Client> list3 = this.SessionFactory.GetCurrentSession().CreateQuery("from Client").List<Client>();
            Assert.AreEqual(1, list2.Count());
            Client client4 = list3[0];
            Assert.AreEqual(2, client4.Accounts.Count());
        }
    }
}
