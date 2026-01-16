using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class EstateDaoTests : BaseTest
    {
        public IEstateDao EstateDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.EstateDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Estate testate = new Estate();
            testate.Name = "Royal Estate";
            this.EstateDao.SaveEntity(testate);

            Estate sestate = new Estate();
            sestate.Name = "Majestic Estate";
            this.EstateDao.SaveEntity(sestate);

            Assert.AreEqual(2, this.EstateDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Estate sestate = new Estate();
            sestate.Name = "Majestic Estate";
            this.EstateDao.SaveEntity(sestate);

            IList<Estate> pList = this.EstateDao.LoadAllEntities();
            Estate estate = pList[0];

            Estate estate2 = this.EstateDao.LoadEntity(estate.Id);
            Assert.AreEqual("Majestic Estate", estate2.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Estate sestate = new Estate();
            sestate.Name = "Majestic Estate";
            this.EstateDao.SaveEntity(sestate);

            Estate testate = new Estate();
            testate.Name = "Royal Estate";
            this.EstateDao.SaveEntity(testate);

            Assert.AreEqual(2, this.EstateDao.LoadAllEntities().Count);
            this.EstateDao.DeleteEntity(testate);
            Assert.AreEqual(1, this.EstateDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Estate sestate = new Estate();
            sestate.Name = "Majestic Estate";
            this.EstateDao.SaveEntity(sestate);

            Assert.AreEqual(1, this.EstateDao.LoadAllEntities().Count);

            IList<Estate> pList = this.EstateDao.LoadAllEntities();
            Estate estate = pList[0];
            estate.Name = "Royal Estate";

            this.EstateDao.UpdateEntity(estate);

            IList<Estate> pList2 = this.EstateDao.LoadAllEntities();
            Estate estate2 = pList2[0];
            Assert.AreEqual("Royal Estate", estate2.Name);
        }
    }
}
