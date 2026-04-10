using com.company.spancy.backend.dao;
using com.company.spancy.backend.entity;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class PersonDaoTests : BaseTest
    {
        public IBaseDao<Person> PersonDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.IsTrue(this.PersonDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            int initialCount = this.PersonDao.LoadAllEntities().Count;

            Person person1 = new Person();
            person1.Name = "John";
            person1.Phones.Add(new Phone { Number = "111-111-1111" });
            person1.Phones.Add(new Phone { Number = "222-222-2222" });
            this.PersonDao.SaveEntity(person1);

            Person person2 = new Person();
            person2.Name = "Mary";
            person2.Phones.Add(new Phone { Number = "333-333-3333" });
            this.PersonDao.SaveEntity(person2);

            // Adjust expectation depending on cascade & how Phone is persisted
            Assert.IsTrue(this.PersonDao.LoadAllEntities().Count >= initialCount + 2);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Person person = new Person();
            person.Name = "John";
            person.Phones.Add(new Phone { Number = "111-111-1111" });
            long id = this.PersonDao.SaveEntity(person);

            Person loaded = this.PersonDao.LoadEntity(id);
            Assert.AreEqual("John", loaded.Name);
            Assert.IsNotNull(loaded.Phones);
            Assert.AreEqual(1, loaded.Phones.Count);
            Assert.AreEqual("111-111-1111", loaded.Phones[0].Number);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int initialCount = this.PersonDao.LoadAllEntities().Count;

            Person person1 = new Person();
            person1.Name = "John";
            person1.Phones.Add(new Phone { Number = "111-111-1111" });
            this.PersonDao.SaveEntity(person1);

            Person person2 = new Person();
            person2.Name = "Mary";
            person2.Phones.Add(new Phone { Number = "333-333-3333" });
            long person2Id = this.PersonDao.SaveEntity(person2);

            Assert.IsTrue(this.PersonDao.LoadAllEntities().Count >= initialCount + 2);

            Person temp = this.PersonDao.LoadEntity(person2Id);
            this.PersonDao.DeleteEntity(temp);

            Assert.IsTrue(this.PersonDao.LoadAllEntities().Count >= initialCount + 1);
            Assert.IsNull(this.PersonDao.LoadEntity(person2Id));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Person person = new Person();
            person.Name = "John";
            person.Phones.Add(new Phone { Number = "111-111-1111" });
            long id = this.PersonDao.SaveEntity(person);

            Person loaded = this.PersonDao.LoadEntity(id);
            loaded.Name = "John Smith";
            loaded.Phones[0].Number = "999-999-9999";
            this.PersonDao.UpdateEntity(loaded);

            Person updated = this.PersonDao.LoadEntity(id);
            Assert.AreEqual("John Smith", updated.Name);
            Assert.AreEqual(1, updated.Phones.Count);
            Assert.AreEqual("999-999-9999", updated.Phones[0].Number);
        }
    }
}