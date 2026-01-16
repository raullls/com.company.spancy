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
    public class UserTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            User u1 = new User();
            u1.Name = "A";
            User u2 = new User();
            u2.Name = "B";
            
            this.SessionFactory.GetCurrentSession().Save(u1);
            this.SessionFactory.GetCurrentSession().Save(u2);
            
            u1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(u1);

            IList<User> list = this.SessionFactory.GetCurrentSession().CreateQuery("from User").List<User>();
            Assert.AreEqual(2, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(u1);
            IList<User> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from User").List<User>();
            Assert.AreEqual(1, list2.Count());

            Group g1 = new Group();
            g1.Name = "A";
            Group g2 = new Group();
            g2.Name = "B";
            
            this.SessionFactory.GetCurrentSession().Save(g1);
            this.SessionFactory.GetCurrentSession().Save(g2);
            
            User u3 = list2[0];
            u3.Groups.Add(g1);
            u3.Groups.Add(g2);
            
            this.SessionFactory.GetCurrentSession().Merge(u3);
            
            IList<User> list3 = this.SessionFactory.GetCurrentSession().CreateQuery("from User").List<User>();
            Assert.AreEqual(1, list3.Count());
            Assert.AreEqual(2, list3[0].Groups.Count());
        }
    }
}
