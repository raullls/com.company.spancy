using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class ManuscriptAuthorServiceTests : BaseTest
    {
        public IManuscriptService ManuscriptService { get; set; }
        public IAuthorService AuthorService { get; set; }
        public IManuscriptAuthorService ManuscriptAuthorService { get; set; }
        public ManuscriptAuthorServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.ManuscriptAuthorService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();
            ManuscriptDto manuscriptDto = new ManuscriptDto();
            manuscriptDto.Name = "The Immortals of Meluha";
            this.ManuscriptService.CreateTX(manuscriptDto);

            AuthorDto authorDto = new AuthorDto();
            authorDto.Name = "Amish Tripathi";
            this.AuthorService.CreateTX(authorDto);

            IList<ManuscriptDto> manuscriptDtos = this.ManuscriptService.FindAllRO();
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];

            IList<AuthorDto> authorDtos = this.AuthorService.FindAllRO();
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            this.ManuscriptAuthorService.CreateTX(manuscriptAuthorDto);
            Assert.AreEqual(1, this.ManuscriptAuthorService.FindAllRO().Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();

            ManuscriptDto manuscriptDto = new ManuscriptDto();
            manuscriptDto.Name = "The Immortals of Meluha";
            this.ManuscriptService.CreateTX(manuscriptDto);

            AuthorDto authorDto = new AuthorDto();
            authorDto.Name = "Amish Tripathi";
            this.AuthorService.CreateTX(authorDto);

            IList<ManuscriptDto> manuscriptDtos = this.ManuscriptService.FindAllRO();
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];

            IList<AuthorDto> authorDtos = this.AuthorService.FindAllRO();
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            this.ManuscriptAuthorService.CreateTX(manuscriptAuthorDto);
            Assert.AreEqual(1, this.ManuscriptAuthorService.FindAllRO().Count);

            IList<ManuscriptAuthorDto> manuscriptAuthorList = this.ManuscriptAuthorService.FindAllRO();
            ManuscriptAuthorDto manuscriptAuthorDto1 = manuscriptAuthorList[0];
            this.ManuscriptAuthorService.RemoveTX(manuscriptAuthorDto1);

            Assert.AreEqual(0, this.ManuscriptAuthorService.FindAllRO().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();

            ManuscriptDto manuscriptDto = new ManuscriptDto();
            manuscriptDto.Name = "The Immortals of Meluha";
            this.ManuscriptService.CreateTX(manuscriptDto);

            AuthorDto authorDto = new AuthorDto();
            authorDto.Name = "Amish Tripathi";
            this.AuthorService.CreateTX(authorDto);

            IList<ManuscriptDto> manuscriptDtos = this.ManuscriptService.FindAllRO();
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];

            IList<AuthorDto> authorDtos = this.AuthorService.FindAllRO();
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            this.ManuscriptAuthorService.CreateTX(manuscriptAuthorDto);
            Assert.AreEqual(1, this.ManuscriptAuthorService.FindAllRO().Count);

            bool status = (bool)this.ManuscriptAuthorService.IsPresentRO(manuscriptAuthorDto);
            Assert.IsTrue(status);
        }
    }
}
