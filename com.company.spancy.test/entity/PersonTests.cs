using com.company.spancy.backend.entity;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class PersonTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Person p1 = new Person();
            p1.Name = "John";
            p1.Phones.Add(new Phone { Number = "111-111-1111" });
            p1.Phones.Add(new Phone { Number = "222-222-2222" });
            this.SessionFactory.GetCurrentSession().Save(p1);

            Person p2 = new Person();
            p2.Name = "Mary";
            p2.Phones.Add(new Phone { Number = "333-333-3333" });
            this.SessionFactory.GetCurrentSession().Save(p2);

            p1.Name = "John Smith";
            p1.Phones[0].Number = "999-999-9999";
            this.SessionFactory.GetCurrentSession().Merge(p1);

            IList<Person> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Person").List<Person>();
            Assert.IsTrue(list.Count > 0);

            this.SessionFactory.GetCurrentSession().Delete(p2);
            list = this.SessionFactory.GetCurrentSession().CreateQuery("from Person").List<Person>();
            Assert.IsTrue(list.Count > 0);
        }
    }
}