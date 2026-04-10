using com.company.spancy.backend;
using com.company.spancy.backend.dto;
using Nancy.Testing;
using Newtonsoft.Json;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class PersonModuleTests : BaseTest
    {
        private static readonly Nancy.Responses.Negotiation.MediaRange JsonMediaType = new Nancy.Responses.Negotiation.MediaRange("application/json");

        [TestMethod]
        public void GetFindAllPersonsTest()
        {
            BrowserResponse response = this.DoGet("/api/onetomanyunidirectional/findAll");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void GetFindPersonByIdTest()
        {
            long personId = 1;
            BrowserResponse response = this.DoGet($"/api/onetomanyunidirectional/findById/{personId}");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostCreatePersonTest()
        {
            PersonDto dto = new PersonDto
            {
                Name = "Dave",
                Numbers = new List<string> { "111-111-1111" }
            };

            BrowserResponse response = this.DoPost("/api/onetomanyunidirectional/create", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostEditPersonTest()
        {
            PersonDto dto = new PersonDto
            {
                Id = 1,
                Name = "Dave",
                Numbers = new List<string> { "111-111-1111" }
            };

            BrowserResponse response = this.DoPost("/api/onetomanyunidirectional/edit", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostRemovePersonTest()
        {
            long personId = 1;
            BrowserResponse response = this.DoPost($"/api/onetomanyunidirectional/remove/{personId}");

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