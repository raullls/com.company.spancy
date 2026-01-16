using com.company.spancy.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class StudentModuleTests : BaseTest
    {
        [TestMethod]
        public void GetFindAllTest()
        {
            Task<BrowserResponse> task = this.Browser.Get("/onetooneselfreference/findAll", with =>
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
            long studentId = 1;
            Task<BrowserResponse> task = this.Browser.Get($"/onetooneselfreference/findById/{studentId}", with =>
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
            StudentDto studentDto = new StudentDto();
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred";

            Task<BrowserResponse> task = this.Browser.Post("/onetooneselfreference/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(studentDto);
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
            long studentId = 1;
            Task<BrowserResponse> task = this.Browser.Post($"/onetooneselfreference/remove/{studentId}", with =>
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
            StudentDto studentDto = new StudentDto();
            studentDto.Id = 1;
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred James";

            Task<BrowserResponse> task = this.Browser.Post("/onetooneselfreference/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(studentDto);
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
