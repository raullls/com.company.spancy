using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class EmployeeDaoTests : BaseTest
    {
        public IEmployeeDao EmployeeDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.EmployeeDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Employee temployee = new Employee();
            temployee.Name = "Lalit Narayan Mishra";
            this.EmployeeDao.SaveEntity(temployee);

            Employee semployee = new Employee();
            semployee.Name = "Amritendu De";
            this.EmployeeDao.SaveEntity(semployee);

            Assert.AreEqual(2, this.EmployeeDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Employee semployee = new Employee();
            semployee.Name = "Lalit Narayan Mishra";
            this.EmployeeDao.SaveEntity(semployee);

            IList<Employee> pList = this.EmployeeDao.LoadAllEntities();
            Employee employee = pList[0];

            Employee employee2 = this.EmployeeDao.LoadEntity(employee.Id);
            Assert.AreEqual("Lalit Narayan Mishra", employee2.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Employee semployee = new Employee();
            semployee.Name = "Lalit Narayan Mishra";
            this.EmployeeDao.SaveEntity(semployee);

            Employee temployee = new Employee();
            temployee.Name = "Amritendu De";
            this.EmployeeDao.SaveEntity(temployee);

            Assert.AreEqual(2, this.EmployeeDao.LoadAllEntities().Count);

            this.EmployeeDao.DeleteEntity(temployee);

            Assert.AreEqual(1, this.EmployeeDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Employee semployee = new Employee();
            semployee.Name = "Lalit Narayan Mishra";
            this.EmployeeDao.SaveEntity(semployee);

            Assert.AreEqual(1, this.EmployeeDao.LoadAllEntities().Count);

            IList<Employee> pList = this.EmployeeDao.LoadAllEntities();
            Employee employee = pList[0];
            employee.Name = "Amritendu De";

            this.EmployeeDao.UpdateEntity(employee);

            IList<Employee> pList2 = this.EmployeeDao.LoadAllEntities();
            Employee employee2 = pList2[0];
            Assert.AreEqual("Amritendu De", employee2.Name);
        }
    }
}
