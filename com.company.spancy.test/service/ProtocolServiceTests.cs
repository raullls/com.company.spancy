using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class ProtocolServiceTests : BaseTest
    {
        public IProtocolService ProtocolService { get; set; }
        public ProtocolServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0L, this.ProtocolService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProtocolDto sprotocol = new SnmpDto();
            sprotocol.Name = "SNMP";
            this.ProtocolService.CreateTX(sprotocol);

            ProtocolDto hProtocol = new TcpDto();
            hProtocol.Name = "TCP/IP";
            this.ProtocolService.CreateTX(hProtocol);

            Assert.AreEqual(2L, this.ProtocolService.FindAllRO().Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ProtocolDto sprotocol = new SnmpDto();
            sprotocol.Name = "SNMP";

            this.ProtocolService.CreateTX(sprotocol);

            IList<ProtocolDto> pList = this.ProtocolService.FindAllRO();
            ProtocolDto protocol = pList[0];
            ProtocolDto protocol2 = this.ProtocolService.FindByIdRO(protocol.Id);

            Assert.AreEqual("SNMP", protocol2.Name);
        }

        [TestMethod]
        public void RemoveTest()
        {
            ProtocolDto sprotocol = new SnmpDto();
            sprotocol.Name = "SNMP";
            this.ProtocolService.CreateTX(sprotocol);

            ProtocolDto hProtocol = new TcpDto();
            hProtocol.Name = "TCP/IP";
            this.ProtocolService.CreateTX(hProtocol);

            Assert.AreEqual(2L, this.ProtocolService.FindAllRO().Count);

            IList<ProtocolDto> pList = this.ProtocolService.FindAllRO();
            ProtocolDto protocol = pList[0];
            this.ProtocolService.RemoveByIdTX(protocol.Id);

            Assert.AreEqual(1L, this.ProtocolService.FindAllRO().Count);
        }

        [TestMethod]
        public void EditTest()
        {
            ProtocolDto sprotocol = new SnmpDto();
            sprotocol.Name = "SNMP";
            this.ProtocolService.CreateTX(sprotocol);

            Assert.AreEqual(1L, this.ProtocolService.FindAllRO().Count);

            IList<ProtocolDto> pList = this.ProtocolService.FindAllRO();
            ProtocolDto protocol = pList[0];
            protocol.Name = "SNMP1";
            this.ProtocolService.UpdateTX(protocol);

            IList<ProtocolDto> pList2 = this.ProtocolService.FindAllRO();
            ProtocolDto protocol2 = pList2[0];

            Assert.AreEqual("SNMP1", protocol2.Name);
        }
    }
}
