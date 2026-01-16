using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class ManuscriptAuthorModuleTests : BaseTest
    {
        public IManuscriptService ManuscriptService { get; set; }
        public IAuthorService AuthorService { get; set; }

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

            IList<ManuscriptDto> manuscriptDtos = (this.ManuscriptService.FindAllRO() as Response).Data as IList<ManuscriptDto>;
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];

            IList<AuthorDto> authorDtos = (this.AuthorService.FindAllRO() as Response).Data as IList<AuthorDto>;
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(manuscriptAuthorDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }
        
        [TestMethod]
        public void PresentTest()
        {
            this.CreateTest();
            ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();

            IList<ManuscriptDto> manuscriptDtos = (this.ManuscriptService.FindAllRO() as Response).Data as IList<ManuscriptDto>;
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];
            IList<AuthorDto> authorDtos = (this.AuthorService.FindAllRO() as Response).Data as IList<AuthorDto>;
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/isPresent", with =>
            {
                with.HttpRequest();
                with.JsonBody(manuscriptAuthorDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void DeleteTest()
        {
            this.CreateTest();
            ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();

            IList<ManuscriptDto> manuscriptDtos = (this.ManuscriptService.FindAllRO() as Response).Data as IList<ManuscriptDto>;
            ManuscriptDto manuscriptDto1 = manuscriptDtos[0];
            IList<AuthorDto> authorDtos = (this.AuthorService.FindAllRO() as Response).Data as IList<AuthorDto>;
            AuthorDto authorDto1 = authorDtos[0];

            manuscriptAuthorDto.ManuscriptDto = manuscriptDto1;
            manuscriptAuthorDto.AuthorDto = authorDto1;
            manuscriptAuthorDto.Publisher = "Createspace";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectionalwithjoinattribute/manuscriptauthor/remove", with =>
            {
                with.HttpRequest();
                with.JsonBody(manuscriptAuthorDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }
    }
}
