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
    public class ItemTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Item item1 = new Item();
            item1.Name = "Book";
            IList<Feature> list = new List<Feature>();
            Feature feature1 = new Feature();
            feature1.Name = "Core Java";
            feature1.Item = item1;
            Feature feature2 = new Feature();
            feature2.Name = "Spring";
            feature2.Item = item1;
            list.Add(feature1);
            list.Add(feature2);
            item1.Features = list;
            
            this.SessionFactory.GetCurrentSession().Save(item1);
            
            Item item2 = new Item();
            item2.Name = "Bags";
            IList<Feature> list2 = new List<Feature>();
            Feature feature3 = new Feature();
            feature3.Name = "SkyBags";
            feature3.Item = item2;
            Feature feature4 = new Feature();
            feature4.Name = "WildCraft";
            feature4.Item = item2;
            list2.Add(feature3);
            list2.Add(feature4);
            item2.Features = list2;
            
            this.SessionFactory.GetCurrentSession().Save(item2);

            IList<Item> items = this.SessionFactory.GetCurrentSession().CreateQuery("select item from Item item order by item.id desc").List<Item>();
            Assert.AreEqual(2, items.Count());
            Item item = items[0];
            item.Name = "BookList";
            this.SessionFactory.GetCurrentSession().Update(item);
            
            IList<Item> itemslist = this.SessionFactory.GetCurrentSession().CreateQuery("select item from Item item order by item.id desc").List<Item>();
            Item pItem = itemslist[0];
            Assert.AreEqual("BookList", pItem.Name);
            this.SessionFactory.GetCurrentSession().Delete(pItem);

            IList<Item> ppItem = this.SessionFactory.GetCurrentSession().CreateQuery("select item from Item item order by item.id desc").List<Item>();
            Assert.AreEqual(1, ppItem.Count());
        }
    }
}
