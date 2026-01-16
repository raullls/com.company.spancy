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
    public class EstateModuleTests : BaseTest
    {
        public IEstateService EstateService { get; set; }

        [TestMethod]
        public void CreateTest()
        {
            EstateDto estateDto = new BuildingDto();
            estateDto.Name = "ABC";
            estateDto.Type = "building";
            ((BuildingDto)estateDto).Floors = 20;

            Task<BrowserResponse> task = this.Browser.Post("/concretetableinheritance/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(estateDto);
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

            IList<EstateDto> estateDtoList = (this.EstateService.FindAllRO() as Response).Data as IList<EstateDto>;
            EstateDto estateDto2 = estateDtoList[0];
            EstateDto newEstateDto = new BuildingDto();
            newEstateDto.Id = estateDto2.Id;
            newEstateDto.Name = "DEF";
            newEstateDto.Type = "building";
            ((BuildingDto)newEstateDto).Floors = 21;

            Task<BrowserResponse> task = this.Browser.Post("/concretetableinheritance/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(newEstateDto);
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

            IList<EstateDto> estateDtoList = (this.EstateService.FindAllRO() as Response).Data as IList<EstateDto>;
            EstateDto estateDto2 = estateDtoList[0];

            Task<BrowserResponse> task = this.Browser.Post($"/concretetableinheritance/remove/{estateDto2.Id}", with =>
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
