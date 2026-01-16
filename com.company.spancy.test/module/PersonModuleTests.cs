using com.company.spancy.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class PersonModuleTests : BaseTest
    {
        [TestMethod]
        public void GetFindAllTest()
        {
            Task<BrowserResponse> task = this.Browser.Get("/onetomanyunidirectional/findAll", with =>
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
            long personId = 1;
            Task<BrowserResponse> task = this.Browser.Get($"/onetomanyunidirectional/findById/{personId}", with =>
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
            PersonDto personDto = new PersonDto();
            personDto.Name = "Alex";
            personDto.Numbers = new List<string>() { "9158798406", "9158798407" };

            Task<BrowserResponse> task = this.Browser.Post("/onetomanyunidirectional/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(personDto);
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
            long personId = 1;
            Task<BrowserResponse> task = this.Browser.Post($"/onetomanyunidirectional/remove/{personId}", with =>
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
            PersonDto personDto = new PersonDto();
            personDto.Id = 1;
            personDto.Name = "Alex";
            personDto.Numbers = new List<string>() { "9150" };

            Task<BrowserResponse> task = this.Browser.Post("/onetomanyunidirectional/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(personDto);
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
