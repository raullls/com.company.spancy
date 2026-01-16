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
    public class ItemModuleTests : BaseTest
    {
        [TestMethod]
        public void GetFindAllTest()
        {
            Task<BrowserResponse> task = this.Browser.Get("/onetomanybidirectional/findAll", with =>
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
            long itemId = 1;
            Task<BrowserResponse> task = this.Browser.Get($"/onetomanybidirectional/findById/{itemId}", with =>
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
            ItemDto itemDto = new ItemDto();
            itemDto.Name = "Book";
            IList<string> featurelist = new List<string>();
            featurelist.Add("Java");
            featurelist.Add("J2ee");
            itemDto.FeatureList = featurelist;

            Task<BrowserResponse> task = this.Browser.Post("/onetomanybidirectional/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(itemDto);
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
            long itemId = 1;
            Task<BrowserResponse> task = this.Browser.Post($"/onetomanybidirectional/remove/{itemId}", with =>
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

            ItemDto itemDto = new ItemDto();
            itemDto.Id = 1;
            itemDto.Name = "Book";
            itemDto.FeatureList = new List<string>() { "Spring 3.0" };

            Task<BrowserResponse> task = this.Browser.Post("/onetomanybidirectional/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(itemDto);
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
