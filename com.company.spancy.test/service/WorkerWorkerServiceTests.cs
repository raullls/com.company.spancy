using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class WorkerWorkerServiceTests : BaseTest
    {
        public IWorkerService WorkerService { get; set; }
        public IWorkerWorkerService WorkerWorkerService { get; set; }
        public WorkerWorkerServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.WorkerWorkerService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();
            WorkerDto workerDto1 = new WorkerDto();
            workerDto1.Name = "The Immortals of Meluha";
            this.WorkerService.CreateTX(workerDto1);

            WorkerDto workerDto2 = new WorkerDto();
            workerDto2.Name = "Amish Tripathi";
            this.WorkerService.CreateTX(workerDto2);

            IList<WorkerDto> workerDtos1 = this.WorkerService.FindAllRO();
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = this.WorkerService.FindAllRO();
            WorkerDto workerDto4 = workerDtos2[1];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Friends";
            this.WorkerWorkerService.CreateTX(workerWorkerDto);

            Assert.AreEqual(1, this.WorkerWorkerService.FindAllRO().Count());
        }

        [TestMethod]
        public void RemoveTest()
        {
            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();

            WorkerDto workerDto1 = new WorkerDto();
            workerDto1.Name = "The Immortals of Meluha";
            this.WorkerService.CreateTX(workerDto1);

            WorkerDto workerDto2 = new WorkerDto();
            workerDto2.Name = "Amish Tripathi";
            this.WorkerService.CreateTX(workerDto2);

            IList<WorkerDto> workerDtos1 = this.WorkerService.FindAllRO();
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = this.WorkerService.FindAllRO();
            WorkerDto workerDto4 = workerDtos2[0];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Friends";
            this.WorkerWorkerService.CreateTX(workerWorkerDto);

            Assert.AreEqual(1, this.WorkerWorkerService.FindAllRO().Count());

            IList<WorkerWorkerDto> workerWorkerList = this.WorkerWorkerService.FindAllRO();
            WorkerWorkerDto workerWorkerDto1 = workerWorkerList[0];
            this.WorkerWorkerService.RemoveTX(workerWorkerDto1);

            Assert.AreEqual(0, this.WorkerWorkerService.FindAllRO().Count());
        }

        [TestMethod]
        public void IsPresentTest()
        {
            WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();

            WorkerDto workerDto1 = new WorkerDto();
            workerDto1.Name = "The Immortals of Meluha";
            this.WorkerService.CreateTX(workerDto1);

            WorkerDto workerDto2 = new WorkerDto();
            workerDto2.Name = "Amish Tripathi";
            this.WorkerService.CreateTX(workerDto2);

            IList<WorkerDto> workerDtos1 = this.WorkerService.FindAllRO();
            WorkerDto workerDto3 = workerDtos1[0];

            IList<WorkerDto> workerDtos2 = this.WorkerService.FindAllRO();
            WorkerDto workerDto4 = workerDtos2[1];

            workerWorkerDto.WorkerId1 = workerDto3;
            workerWorkerDto.WorkerId2 = workerDto4;
            workerWorkerDto.RelationshipType = "Friends";
            this.WorkerWorkerService.CreateTX(workerWorkerDto);

            Assert.AreEqual(1, this.WorkerWorkerService.FindAllRO().Count());
            bool status = (bool)this.WorkerWorkerService.IsPresentRO(workerWorkerDto);
            Assert.IsTrue(status);
        }
    }
}
