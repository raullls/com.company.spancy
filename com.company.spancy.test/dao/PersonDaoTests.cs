using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class PersonDaoTests : BaseTest
    {
        public IPersonDao PersonDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.PersonDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
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

            this.PersonDao.SaveEntity(p1);
            Assert.AreEqual(1, this.PersonDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
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
            this.PersonDao.SaveEntity(p1);

            Person p2 = new Person();
            p2.Name = "Fred";
            IList<Phone> phones2 = new List<Phone>();
            Phone phone1 = new Phone();
            phone1.Number = "8836987";
            phones2.Add(phone1);
            p2.Phones = phones2;
            this.PersonDao.SaveEntity(p2);
            Assert.AreEqual(2, this.PersonDao.LoadAllEntities().Count);

            IList<Person> persons = this.PersonDao.LoadAllEntities();
            Person p3 = persons[1];

            Person p4 = this.PersonDao.LoadEntity(p3.Id);
            Assert.AreEqual("Fred", p4.Name);
        }

        [TestMethod]
        public void DeleteTest()
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
            this.PersonDao.SaveEntity(p1);

            Person p2 = new Person();
            p2.Name = "Fred";
            IList<Phone> phones2 = new List<Phone>();
            Phone phone1 = new Phone();
            phone1.Number = "8836987";
            phones2.Add(phone1);
            p2.Phones = phones2;
            this.PersonDao.SaveEntity(p2);
            Assert.AreEqual(2, this.PersonDao.LoadAllEntities().Count);

            IList<Person> persons = this.PersonDao.LoadAllEntities();
            Person p = persons[1];
            this.PersonDao.DeleteEntity(p);
            Assert.AreEqual(1, this.PersonDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
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
            this.PersonDao.SaveEntity(p1);

            Assert.AreEqual(1, this.PersonDao.LoadAllEntities().Count);

            IList<Person> persons = this.PersonDao.LoadAllEntities();
            Person p = persons[0];
            p.Name = "John";
            IList<Phone> phones1 = new List<Phone>();
            Phone ph3 = new Phone();
            ph3.Number = "111111111";
            Phone ph4 = new Phone();
            ph4.Number = "222222222";
            Phone ph5 = new Phone();
            ph5.Number = "333333333";
            phones1.Add(ph3);
            phones1.Add(ph4);
            phones1.Add(ph5);
            p.Phones = phones1;
            this.PersonDao.UpdateEntity(p);

            persons = this.PersonDao.LoadAllEntities();
            Person p2 = persons[0];
            Assert.AreEqual("John", p2.Name);
            Assert.AreEqual(3, p2.Phones.Count);
        }
    }
}
