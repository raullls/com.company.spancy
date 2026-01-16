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
    public class EmployeeTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Employee e1 = new Employee();
            e1.Name = "A";

            Employee e2 = new Employee();
            e2.Name = "B";

            this.SessionFactory.GetCurrentSession().Save(e1);
            this.SessionFactory.GetCurrentSession().Save(e2);

            e1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(e1);

            IList<Employee> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Employee").List<Employee>();
            Assert.AreEqual(2L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(e1);

            IList<Employee> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Employee").List<Employee>();
            Assert.AreEqual(1L, list2.Count());
        }
    }
}
