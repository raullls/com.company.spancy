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
    public class EmployeeServiceTests : BaseTest
    {
        public IEmployeeService EmployeeService { get; set; }
        public EmployeeServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0L, this.EmployeeService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            EmployeeDto bemployee = new PermanentEmployeeDto();
            bemployee.Name = "Lalit Narayan Mishra";
            ((PermanentEmployeeDto)bemployee).Leaves = 30;
            ((PermanentEmployeeDto)bemployee).Salary = 500000;
            this.EmployeeService.CreateTX(bemployee);

            EmployeeDto hEmployee = new ContractorEmployeeDto();
            hEmployee.Name = "Amritendu De";
            ((ContractorEmployeeDto)hEmployee).HourleyRate = 35;
            ((ContractorEmployeeDto)hEmployee).OvertimeRate = 20;
            this.EmployeeService.CreateTX(hEmployee);

            Assert.AreEqual(2L, this.EmployeeService.FindAllRO().Count());
        }

        [TestMethod]
        public void FindByIdTest()
        {
            EmployeeDto bemployee = new PermanentEmployeeDto();
            bemployee.Name = "Lalit Narayan Mishra";
            ((PermanentEmployeeDto)bemployee).Leaves = 30;
            ((PermanentEmployeeDto)bemployee).Salary = 500000;
            this.EmployeeService.CreateTX(bemployee);

            IList<EmployeeDto> pList = this.EmployeeService.FindAllRO();
            EmployeeDto employee = pList[0];

            EmployeeDto employee2 = this.EmployeeService.FindByIdRO(employee.Id);
            Assert.AreEqual("Lalit Narayan Mishra", employee2.Name);
        }

        [TestMethod]
        public void RemoveTest()
        {
            EmployeeDto bemployee = new PermanentEmployeeDto();
            bemployee.Name = "Lalit Narayan Mishra";
            ((PermanentEmployeeDto)bemployee).Leaves = 30;
            ((PermanentEmployeeDto)bemployee).Salary = 500000;
            this.EmployeeService.CreateTX(bemployee);

            EmployeeDto hEmployee = new ContractorEmployeeDto();
            hEmployee.Name = "Amritendu De";
            ((ContractorEmployeeDto)hEmployee).HourleyRate = 35;
            ((ContractorEmployeeDto)hEmployee).OvertimeRate = 20;
            this.EmployeeService.CreateTX(hEmployee);

            Assert.AreEqual(2L, this.EmployeeService.FindAllRO().Count());

            IList<EmployeeDto> pList = this.EmployeeService.FindAllRO();
            EmployeeDto employee = pList[0];
            this.EmployeeService.RemoveByIdTX(employee.Id);

            Assert.AreEqual(1L, this.EmployeeService.FindAllRO().Count());
        }

        [TestMethod]
        public void EditTest()
        {
            EmployeeDto bemployee = new PermanentEmployeeDto();
            bemployee.Name = "Lalit Narayan Mishra";
            ((PermanentEmployeeDto)bemployee).Leaves = 30;
            ((PermanentEmployeeDto)bemployee).Salary = 500000;
            this.EmployeeService.CreateTX(bemployee);

            Assert.AreEqual(1L, this.EmployeeService.FindAllRO().Count());

            IList<EmployeeDto> pList = this.EmployeeService.FindAllRO();
            EmployeeDto employee = pList[0];
            employee.Name = "Amritendu De";

            this.EmployeeService.UpdateTX(employee);

            IList<EmployeeDto> pList2 = this.EmployeeService.FindAllRO();
            EmployeeDto employee2 = pList2[0];
            Assert.AreEqual("Amritendu De", employee2.Name);
        }
    }
}
