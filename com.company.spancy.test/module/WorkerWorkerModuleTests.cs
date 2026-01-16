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
    public class WorkerWorkerModuleTests : BaseTest
    {
        public IWorkerService WorkerService { get; set; }

        [TestMethod]
        public void CreateTest()
        {
            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();

            WorkerDto workerDto1 = new WorkerDto();
            workerDto1.Name = "Amritendu De";
            this.WorkerService.CreateTX(workerDto1);

            WorkerDto workerDto2 = new WorkerDto();
            workerDto2.Name = "Amish Tripathi";
            this.WorkerService.CreateTX(workerDto2);

            IList<WorkerDto> workerDtos1 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto4 = workerDtos2[1];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Authors";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreferencewithjoinattribute/workerworker/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(workerWorkerDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            this.CreateTest();

            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();

            IList<WorkerDto> workerDtos1 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto4 = workerDtos2[1];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Authors";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreferencewithjoinattribute/workerworker/isPresent", with =>
            {
                with.HttpRequest();
                with.JsonBody(workerWorkerDto);
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

            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();

            IList<WorkerDto> workerDtos1 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = (this.WorkerService.FindAllRO() as Response).Data as IList<WorkerDto>;
            WorkerDto workerDto4 = workerDtos2[1];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Authors";

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreferencewithjoinattribute/workerworker/remove", with =>
            {
                with.HttpRequest();
                with.JsonBody(workerWorkerDto);
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
