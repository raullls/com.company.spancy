using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class PersonTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Person p1 = new Person();
            p1.Name = "Alex";
            IList<Phone> phones = new List<Phone>();
            Phone ph1 = new Phone();
            ph1.Number = "7798989138";
            Phone ph2 = new Phone();
            ph2.Number = "7798989169";
            phones.Add(ph1);
            phones.Add(ph2);
            p1.Phones = phones;
            this.SessionFactory.GetCurrentSession().Save(p1);

            Person p2 = new Person();
            p2.Name = "Fred";
            IList<Phone> phones2 = new List<Phone>();
            Phone phone1 = new Phone();
            phone1.Number = "8836987";
            phones2.Add(phone1);
            p2.Phones = phones2;
            this.SessionFactory.GetCurrentSession().Save(p2);

            IList<Person> persons = this.SessionFactory.GetCurrentSession().CreateQuery("select p from Person p order by id asc").List<Person>();
            Assert.AreEqual(2, persons.Count);
            
            Person p = persons[0];
            p.Name = "Jhonson";
            this.SessionFactory.GetCurrentSession().Update(p);

            IList<Person> persons2 = this.SessionFactory.GetCurrentSession().CreateQuery("select p from Person p order by id asc").List<Person>();
            Person tp = persons2[0];
            Assert.AreEqual("Jhonson", tp.Name);
            this.SessionFactory.GetCurrentSession().Delete(tp);
            
            IList<Person> persons3 = this.SessionFactory.GetCurrentSession().CreateQuery("select p from Person p order by id asc").List<Person>();
            Assert.AreEqual(1, persons3.Count);
        }
    }
}
