using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class PersonServiceTests : BaseTest
    {
        public IPersonService PersonService { get; set; } = null!;

        public PersonServiceTests() : base(true) {}

        [TestMethod]
        public void FindAllTest()
        {
            Assert.IsTrue(this.PersonService.FindAllRO().Count > 0);
        }

        [TestMethod]
        public void CreatePersonTest()
        {
            PersonDto dto = new PersonDto
            {
                Name = "Alice",
                Numbers = new List<string> { "111-111-1111", "222-222-2222" }
            };
            long id = (long)this.PersonService.CreatePersonTX(dto);

            PersonDto loaded = this.PersonService.FindByIdRO(id);
            Assert.AreEqual("Alice", loaded.Name);
            Assert.IsNotNull(loaded.Numbers);
            Assert.AreEqual(2, loaded.Numbers.Count);
            Assert.IsTrue(loaded.Numbers.Contains("111-111-1111"));
            Assert.IsTrue(loaded.Numbers.Contains("222-222-2222"));
        }

        [TestMethod]
        public void FindByIdTest()
        {
            PersonDto dto = new PersonDto
            {
                Name = "Bob",
                Numbers = new List<string> { "333-333-3333" }
            };
            long id = (long)this.PersonService.CreatePersonTX(dto);

            PersonDto loaded = this.PersonService.FindByIdRO(id);
            Assert.AreEqual("Bob", loaded.Name);
            Assert.AreEqual(1, loaded.Numbers.Count);
            Assert.AreEqual("333-333-3333", loaded.Numbers[0]);
        }

        [TestMethod]
        public void UpdatePersonTXTest()
        {
            PersonDto createDto = new PersonDto
            {
                Name = "Carol",
                Numbers = new List<string> { "444-444-4444" }
            };
            long id = (long)this.PersonService.CreatePersonTX(createDto);

            PersonDto toUpdate = this.PersonService.FindByIdRO(id);
            toUpdate.Name = "Carol Updated";
            toUpdate.Numbers = new List<string> { "555-555-5555", "666-666-6666" };

            this.PersonService.UpdatePersonTX(toUpdate);

            PersonDto updated = this.PersonService.FindByIdRO(id);
            Assert.AreEqual("Carol Updated", updated.Name);
            Assert.AreEqual(2, updated.Numbers.Count);
            Assert.IsTrue(updated.Numbers.Contains("555-555-5555"));
            Assert.IsTrue(updated.Numbers.Contains("666-666-6666"));
        }

        [TestMethod]
        public void RemovePersonTXTest()
        {
            PersonDto dto = new PersonDto
            {
                Name = "Dave",
                Numbers = new List<string> { "777-777-7777" }
            };
            long id = (long)this.PersonService.CreatePersonTX(dto);

            PersonDto loaded = this.PersonService.FindByIdRO(id);
            Assert.IsNotNull(loaded);

            this.PersonService.RemovePersonTX(id);

            IList<PersonDto> remaining = this.PersonService.FindAllRO();
            Assert.IsTrue(remaining.All(p => p.Id != id));
        }
    }
}