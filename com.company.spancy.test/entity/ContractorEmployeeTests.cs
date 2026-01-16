using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class ContractorEmployeeTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Employee e1 = new Employee();
            e1.Name = "A";
            this.SessionFactory.GetCurrentSession().Save(e1);

            ContractorEmployee contractorEmployee = new ContractorEmployee();
            contractorEmployee.Name = "Lalit Narayan Mishra";
            contractorEmployee.HourleyRate = 35;
            contractorEmployee.OvertimeRate = 15;
            this.SessionFactory.GetCurrentSession().Save(contractorEmployee);

            contractorEmployee.Name = "Amritendu De";
            this.SessionFactory.GetCurrentSession().Merge(contractorEmployee);

            IList<ContractorEmployee> list = this.SessionFactory.GetCurrentSession().CreateQuery("from ContractorEmployee").List<ContractorEmployee>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(contractorEmployee);

            IList<ContractorEmployee> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from ContractorEmployee").List<ContractorEmployee>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
