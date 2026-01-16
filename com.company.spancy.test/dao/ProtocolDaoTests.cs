using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class ProtocolDaoTests : BaseTest
    {
        public IProtocolDao ProtocolDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.ProtocolDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Protocol tprotocol = new Tcp();
            tprotocol.Name = "TCP/IP";
            this.ProtocolDao.SaveEntity(tprotocol);

            Protocol sprotocol = new Snmp();
            sprotocol.Name = "SNMP";
            this.ProtocolDao.SaveEntity(sprotocol);

            Protocol protocol = new Protocol();
            protocol.Name = "PROTOCOL";
            this.ProtocolDao.SaveEntity(protocol);

            Assert.AreEqual(3, this.ProtocolDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Protocol sprotocol = new Snmp();
            sprotocol.Name = "SNMP";
            this.ProtocolDao.SaveEntity(sprotocol);

            IList<Protocol> pList = this.ProtocolDao.LoadAllEntities();
            Protocol protocol = pList[0];
            Assert.IsInstanceOfType(protocol, typeof(Snmp));

            Protocol protocol2 = this.ProtocolDao.LoadEntity(protocol.Id);

            Assert.AreEqual("SNMP", protocol2.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Protocol sprotocol = new Snmp();
            sprotocol.Name = "SNMP";
            this.ProtocolDao.SaveEntity(sprotocol);

            Protocol tprotocol = new Tcp();
            tprotocol.Name = "TCP/IP";
            this.ProtocolDao.SaveEntity(tprotocol);

            Assert.AreEqual(2, this.ProtocolDao.LoadAllEntities().Count);

            this.ProtocolDao.DeleteEntity(tprotocol);

            Assert.AreEqual(1, this.ProtocolDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Protocol sprotocol = new Snmp();
            sprotocol.Name = "SNMP";
            this.ProtocolDao.SaveEntity(sprotocol);

            Assert.AreEqual(1, this.ProtocolDao.LoadAllEntities().Count);

            IList<Protocol> pList = this.ProtocolDao.LoadAllEntities();
            Protocol protocol = pList[0];
            protocol.Name = "TCP/IP";
            this.ProtocolDao.UpdateEntity(protocol);

            IList<Protocol> pList2 = this.ProtocolDao.LoadAllEntities();
            Protocol protocol2 = pList2[0];

            Assert.AreEqual("TCP/IP", protocol2.Name);
        }
    }
}
