using com.company.spancy.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Json;
using Nancy.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class CustomerModuleTests : BaseTest
    {
        [TestMethod]
        public void GetFindAllTest()
        {
            Task<BrowserResponse> task = this.Browser.Get("/onetoonebidirectional/findAll", with =>
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
            long customerId = 1;
            Task<BrowserResponse> task = this.Browser.Get($"/onetoonebidirectional/findById/{customerId}", with =>
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
            CustomerDto customerDto = new CustomerDto();
            customerDto.Amount = 400;
            customerDto.Name = "Alex";

            Task<BrowserResponse> task = this.Browser.Post("/onetoonebidirectional/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(customerDto);
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
            long customerId = 1;
            Task<BrowserResponse> task = this.Browser.Post($"/onetoonebidirectional/remove/{customerId}", with =>
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
            CustomerDto customerDto = new CustomerDto();
            customerDto.Id = 1;
            customerDto.Amount = 400;
            customerDto.Name = "Alex";

            Task<BrowserResponse> task = this.Browser.Post("/onetoonebidirectional/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(customerDto);
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
