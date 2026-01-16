using com.company.spancy.dto;
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
    public class CategoryModuleTests : BaseTest
    {
        [TestMethod]
        public void GetFindAllTest()
        {
            Task<BrowserResponse> task = this.Browser.Get("/onetomanyselfreference/findAll", with =>
            {
                with.HttpRequest();
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void GetFindByIdTest()
        {
            long categoryId = 1;
            Task<BrowserResponse> task = this.Browser.Get($"/onetomanyselfreference/findById/{categoryId}", with =>
            {
                with.HttpRequest();
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void PostCreateTest()
        {
            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Name = "Book";

            Task<BrowserResponse> task = this.Browser.Post("/onetomanyselfreference/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(categoryDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void PostRemoveTest()
        {
            long categoryId = 1;
            Task<BrowserResponse> task = this.Browser.Post($"/onetomanyselfreference/remove/{categoryId}", with =>
            {
                with.HttpRequest();
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void PostEditTest()
        {
            this.PostCreateTest();

            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Id = 1;
            categoryDto.Name = "EBook";

            Task<BrowserResponse> task = this.Browser.Post("/onetomanyselfreference/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(categoryDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);

            this.GetFindByIdTest();
        }
    }
}
