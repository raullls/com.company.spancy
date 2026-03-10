using com.company.spancy.backend;
using com.company.spancy.backend.dto;
using Nancy.Testing;
using Newtonsoft.Json;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class CustomerModuleTests : BaseTest
    {
        private static readonly Nancy.Responses.Negotiation.MediaRange JsonMediaType = new Nancy.Responses.Negotiation.MediaRange("application/json");

        [TestMethod]
        public void GetFindAllCustomersTest()
        {
            BrowserResponse response = this.DoGet("/api/onetoonebidirectional/findAll");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void GetFindCustomerByIdTest()
        {
            long customerId = 1;
            BrowserResponse response = this.DoGet($"/api/onetoonebidirectional/findById/{customerId}");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostCreateCustomerTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Name = "Module Test Customer",
                Amount = 150.25
            };

            BrowserResponse response = this.DoPost("/api/onetoonebidirectional/create", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostEditCustomerTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Id = 1,
                Name = "Module Test Customer Updated",
                Amount = 175.00
            };

            BrowserResponse response = this.DoPost("/api/onetoonebidirectional/edit", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostRemoveCustomerTest()
        {
            long customerId = 1;
            BrowserResponse response = this.DoPost($"/api/onetoonebidirectional/remove/{customerId}");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        private BrowserResponse DoGet(string path)
        {
            Task<BrowserResponse> task = this.Browser.Get(path, with =>
            {
                with.HttpRequest();
                with.Accept(JsonMediaType);
            });
            return task.Result;
        }

        private BrowserResponse DoPost(string path, object? body = null)
        {
            Task<BrowserResponse> task = this.Browser.Post(path, with =>
            {
                with.HttpRequest();
                if (body != null)
                {
                    with.JsonBody(body);
                }
                with.Accept(JsonMediaType);
            });
            return task.Result;
        }

        private Response ReadEnvelope(BrowserResponse response)
        {
            string json = response.Body.AsString();
            return JsonConvert.DeserializeObject<Response>(json)!;
        }
    }
}
