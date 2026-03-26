using com.company.spancy.backend;
using com.company.spancy.backend.dto;
using Nancy.Testing;
using Newtonsoft.Json;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class StudentModuleTests : BaseTest
    {
        private static readonly Nancy.Responses.Negotiation.MediaRange JsonMediaType = new Nancy.Responses.Negotiation.MediaRange("application/json");

        [TestMethod]
        public void GetFindAllStudentsTest()
        {
            BrowserResponse response = this.DoGet("/api/onetooneselfreference/findAll");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void GetFindStudentByIdTest()
        {
            long studentId = 1;
            BrowserResponse response = this.DoGet($"/api/onetooneselfreference/findById/{studentId}");

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNull(envelope.Errors);
        }

        [TestMethod]
        public void PostCreateStudentTest()
        {
            StudentDto dto = new StudentDto
            {
                Name = "Dave",
                MentorName = "Mentor Dave"
            };

            BrowserResponse response = this.DoPost("/api/onetooneselfreference/create", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.Conflict, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNotNull(envelope.Errors);
        }

        [TestMethod]
        public void PostEditStudentTest()
        {
            StudentDto dto = new StudentDto
            {
                Id = 1,
                Name = "Dave",
                MentorName = "Mentor Dave"
            };

            BrowserResponse response = this.DoPost("/api/onetooneselfreference/edit", dto);

            Assert.AreEqual(Nancy.HttpStatusCode.Conflict, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNotNull(envelope.Errors);
        }

        [TestMethod]
        public void PostRemoveStudentTest()
        {
            long studentId = 1;
            BrowserResponse response = this.DoPost($"/api/onetooneselfreference/remove/{studentId}");

            Assert.AreEqual(Nancy.HttpStatusCode.Conflict, response.StatusCode);
            Response envelope = this.ReadEnvelope(response);
            Assert.IsNotNull(envelope.Errors);
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
