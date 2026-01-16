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
    public class MemberTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Member member1 = new Member();
            member1.Name = "Amritendu De";

            Member member2 = new Member();
            member2.Name = "Lalit Narayan Mishra";

            Member member3 = new Member();
            member3.Name = "Hazekul Alam";

            this.SessionFactory.GetCurrentSession().Save(member1);
            this.SessionFactory.GetCurrentSession().Save(member2);
            this.SessionFactory.GetCurrentSession().Save(member3);

            member1.Name = "Amish Tripathi";
            this.SessionFactory.GetCurrentSession().Merge(member1);

            IList<Member> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Member").List<Member>();
            Assert.AreEqual(3, list.Count);

            this.SessionFactory.GetCurrentSession().Delete(member1);

            IList<Member> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Member").List<Member>();
            Assert.AreEqual(2, list2.Count);

            member2.Members.Add(member3);
            this.SessionFactory.GetCurrentSession().Merge(member2);

            IList<Member> list3 = this.SessionFactory.GetCurrentSession().CreateQuery("select distinct m from Member m join m.Members m1").List<Member>();
            Assert.AreEqual(1, list3.Count);

            Member member4 = list3[0];
            Assert.AreEqual(1, member4.Members.Count);
        }
    }
}
