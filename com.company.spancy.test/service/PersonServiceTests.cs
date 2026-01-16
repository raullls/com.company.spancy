using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class PersonServiceTests : BaseTest
    {
        public IPersonService PersonService { get; set; }

        public PersonServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.PersonService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            PersonDto personDto1 = new PersonDto();
            personDto1.Name = "Alex";
            IList<string> phones = new List<string>();
            phones.Add("9158798405");
            phones.Add("7798989134");
            personDto1.Numbers = phones;
            this.PersonService.CreatePersonTX(personDto1);
            Assert.AreEqual(1, this.PersonService.FindAllRO().Count);

            IList<PersonDto> personDtos = this.PersonService.FindAllRO();
            PersonDto dto = personDtos[0];
            Assert.AreEqual(2, dto.Numbers.Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            PersonDto personDto1 = new PersonDto();
            personDto1.Name = "Alex";
            IList<string> phones = new List<string>();
            phones.Add("9158798405");
            phones.Add("7798989134");
            personDto1.Numbers = phones;
            this.PersonService.CreatePersonTX(personDto1);

            IList<PersonDto> personDtos = this.PersonService.FindAllRO();
            PersonDto dto = personDtos[0];
            PersonDto dto1 = this.PersonService.FindByIdRO(dto.Id);
            Assert.AreEqual("Alex", dto1.Name);
            Assert.AreEqual(2, dto1.Numbers.Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            PersonDto personDto1 = new PersonDto();
            personDto1.Name = "Alex";
            IList<string> phones = new List<string>();
            phones.Add("9158798405");
            phones.Add("7798989134");
            personDto1.Numbers = phones;
            this.PersonService.CreatePersonTX(personDto1);
            PersonDto personDto2 = new PersonDto();
            personDto2.Name = "Fred";
            IList<string> phones2 = new List<string>();
            phones2.Add("9158798408");
            phones2.Add("7798989139");
            personDto2.Numbers = phones2;
            this.PersonService.CreatePersonTX(personDto2);
            Assert.AreEqual(2, this.PersonService.FindAllRO().Count);
            
            IList<PersonDto> personDtos = this.PersonService.FindAllRO();
            PersonDto deletePerson = personDtos[1];
            this.PersonService.RemoveByIdTX(deletePerson.Id);
            Assert.AreEqual(1, this.PersonService.FindAllRO().Count);
        }

        [TestMethod]
        public void EditTest()
        {
            PersonDto personDto1 = new PersonDto();
            personDto1.Name = "Alex";
            IList<string> phones = new List<string>();
            phones.Add("9158798405");
            phones.Add("7798989134");
            personDto1.Numbers = phones;
            this.PersonService.CreatePersonTX(personDto1);
            Assert.AreEqual(1, this.PersonService.FindAllRO().Count);

            IList<PersonDto> personDtos = this.PersonService.FindAllRO();
            PersonDto dto = personDtos[0];
            phones = new List<string>();
            phones.Add("9158798406");
            phones.Add("9158798407");
            phones.Add("9158798408");
            dto.Numbers = phones;
            this.PersonService.UpdatePersonTX(dto);
            Assert.AreEqual(3, this.PersonService.FindAllRO()[0].Numbers.Count);
        }
    }
}
