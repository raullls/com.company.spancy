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
    public class EstateServiceTests : BaseTest
    {
        public IEstateService EstateService { get; set; }
        public EstateServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0L, this.EstateService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            EstateDto bestate = new BuildingDto();
            bestate.Name = "Majestic Estate";
            ((BuildingDto)bestate).Floors = 10;
            this.EstateService.CreateTX(bestate);

            EstateDto hEstate = new LandDto();
            hEstate.Name = "Royal Estate";
            ((LandDto)hEstate).Area = 200;
            this.EstateService.CreateTX(hEstate);

            Assert.AreEqual(2L, this.EstateService.FindAllRO().Count());
        }

        [TestMethod]
        public void FindByIdTest()
        {
            EstateDto bestate = new BuildingDto();
            bestate.Name = "Majestic Estate";
            ((BuildingDto)bestate).Floors = 10;
            this.EstateService.CreateTX(bestate);

            IList<EstateDto> pList = this.EstateService.FindAllRO();
            EstateDto estate = pList[0];

            EstateDto estate2 = this.EstateService.FindByIdRO(estate.Id);
            Assert.AreEqual("Majestic Estate", estate2.Name);
        }

        [TestMethod]
        public void RemoveTest()
        {
            EstateDto bestate = new BuildingDto();
            bestate.Name = "Majestic Estate";
            ((BuildingDto)bestate).Floors = 10;
            this.EstateService.CreateTX(bestate);

            EstateDto hEstate = new LandDto();
            hEstate.Name = "Royal Estate";
            ((LandDto)hEstate).Area = 200;
            this.EstateService.CreateTX(hEstate);

            Assert.AreEqual(2L, this.EstateService.FindAllRO().Count());

            IList<EstateDto> pList = this.EstateService.FindAllRO();
            EstateDto estate = pList[0];
            this.EstateService.RemoveByIdTX(estate.Id);

            Assert.AreEqual(1L, this.EstateService.FindAllRO().Count());
        }

        [TestMethod]
        public void EditTest()
        {
            EstateDto bestate = new BuildingDto();
            bestate.Name = "Majestic Estate";
            ((BuildingDto)bestate).Floors = 10;
            this.EstateService.CreateTX(bestate);

            Assert.AreEqual(1L, this.EstateService.FindAllRO().Count());

            IList<EstateDto> pList = this.EstateService.FindAllRO();
            EstateDto estate = pList[0];
            estate.Name = "Royal Estate";

            this.EstateService.UpdateTX(estate);

            IList<EstateDto> pList2 = this.EstateService.FindAllRO();
            EstateDto estate2 = pList2[0];
            Assert.AreEqual("Royal Estate", estate2.Name);
        }
    }
}
