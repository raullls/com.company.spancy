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
    public class ProtocolModuleTests : BaseTest
    {
        public IProtocolService ProtocolService { get; set; }

        [TestMethod]
        public void CreateTest()
        {
            ProtocolDto protocolDto = new TcpDto();
            protocolDto.Type = "tcp";
            protocolDto.Name = "ABC";

            Task<BrowserResponse> task = this.Browser.Post("/singletableinheritance/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(protocolDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void UpdateTest()
        {
            this.CreateTest();

            IList<ProtocolDto> protocolDtoList = (this.ProtocolService.FindAllRO() as Response).Data as IList<ProtocolDto>;
            ProtocolDto protocolDto2 = protocolDtoList[0];
            protocolDto2.Name = "DEF";

            Task<BrowserResponse> task = this.Browser.Post("/singletableinheritance/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(protocolDto2);
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

            IList<ProtocolDto> protocolDtoList = (this.ProtocolService.FindAllRO() as Response).Data as IList<ProtocolDto>;
            ProtocolDto protocolDto2 = protocolDtoList[0];

            Task<BrowserResponse> task = this.Browser.Post($"/singletableinheritance/remove/{protocolDto2.Id}", with =>
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
    }
}
