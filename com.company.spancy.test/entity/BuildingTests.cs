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
    public class BuildingTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Estate p1 = new Estate();
            p1.Name = "Estate";
            this.SessionFactory.GetCurrentSession().Save(p1);

            Building building = new Building();
            building.Name = "Building";
            building.Floors = 20;
            this.SessionFactory.GetCurrentSession().Save(building);

            building.Name = "Building Estate";
            building.Floors = 30;
            this.SessionFactory.GetCurrentSession().Merge(building);

            IList<Building> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Building").List<Building>();
            Assert.AreEqual(1L, list.Count());

            this.SessionFactory.GetCurrentSession().Delete(building);

            IList<Building> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Building").List<Building>();
            Assert.AreEqual(0L, list2.Count());
        }
    }
}
