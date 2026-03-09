using com.company.spancy.backend;
using com.company.spancy.backend.dto;
using Nancy.Testing;
using Newtonsoft.Json;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class BookModuleTests : BaseTest
    {
        private static readonly Nancy.Responses.Negotiation.MediaRange JsonMediaType = new Nancy.Responses.Negotiation.MediaRange("application/json");

        [TestMethod]
        public void GetFindAllBooksTest()
        {
            BrowserResponse response = this.DoGet("/onetooneunidirectional/findAll");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void GetFindBookByIdTest()
        {
            long bookId = 1;

            BrowserResponse response = this.DoGet($"/onetooneunidirectional/findById/{bookId}");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostCreateBookTest()
        {
            BookDto dto = new BookDto
            {
                Name = "Module Test Book B",
                City = "Barcelona"
            };
            BrowserResponse response = this.DoPost("/onetooneunidirectional/create", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostEditBookTest()
        {
            BookDto editDto = new BookDto
            {
                Id = 1,
                Name = "Module Test Book C Updated",
                City = "Seville"
            };

            BrowserResponse response = this.DoPost("/onetooneunidirectional/edit", editDto);

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostRemoveBookTest()
        {
            long bookId = 1;

            BrowserResponse response = this.DoPost($"/onetooneunidirectional/remove/{bookId}");

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
