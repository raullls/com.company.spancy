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
    public class PermanentEmployeeTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Employee e1 = new Employee();
            e1.Name = "A";
            this.SessionFactory.GetCurrentSession().Save(e1);

            PermanentEmployee permanentEmployee = new PermanentEmployee();
            permanentEmployee.Name = "Lalit Narayan Mishra";
            permanentEmployee.Leaves = 20;
            permanentEmployee.Salary = 500000;
            this.SessionFactory.GetCurrentSession().Save(permanentEmployee);

            permanentEmployee.Name = "Amritendu De";
            this.SessionFactory.GetCurrentSession().Merge(permanentEmployee);

            IList<PermanentEmployee> list = this.SessionFactory.GetCurrentSession().CreateQuery("from PermanentEmployee").List<PermanentEmployee>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(permanentEmployee);

            IList<PermanentEmployee> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from PermanentEmployee").List<PermanentEmployee>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
