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
    public class ItemDaoTests : BaseTest
    {
        public IItemDao ItemDao { get; set; }
        
        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.ItemDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Item item = new Item();
            item.Name = "Book";
            IList<Feature> list = new List<Feature>();
            Feature feature1 = new Feature();
            feature1.Name = "Core Java";
            feature1.Item = item;
            Feature feature2 = new Feature();
            feature2.Name = "Spring";
            feature2.Item = item;
            list.Add(feature1);
            list.Add(feature2);
            item.Features = list;
            this.ItemDao.SaveEntity(item);
            Assert.AreEqual(1, this.ItemDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
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
            this.ItemDao.SaveEntity(item1);

            Assert.AreEqual(1, this.ItemDao.LoadAllEntities().Count);

            IList<Item> items = this.ItemDao.LoadAllEntities();
            Item item2 = items[0];
            Item item3 = this.ItemDao.LoadEntity(item2.Id);

            Assert.AreEqual("Book", item3.Name);
            Assert.AreEqual(2, item3.Features.Count);
        }

        [TestMethod]
        public void DeleteTest()
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

            this.ItemDao.SaveEntity(item1);

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

            this.ItemDao.SaveEntity(item2);

            Assert.AreEqual(2, this.ItemDao.LoadAllEntities().Count);

            IList<Item> items = this.ItemDao.LoadAllEntities();
            Item item = items[0];
            this.ItemDao.DeleteEntity(item);
            Assert.AreEqual(1, this.ItemDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
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

            this.ItemDao.SaveEntity(item1);

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

            this.ItemDao.SaveEntity(item2);

            IList<Item> items = this.ItemDao.LoadAllEntities();
            Item item = items[1];
            Feature feature = item.Features[1];
            feature.Name = "Spring 3.2";
            this.ItemDao.UpdateEntity(item);
            items = this.ItemDao.LoadAllEntities();
            item = items[1];
            Assert.AreEqual("Spring 3.2", item.Features[1].Name);
        }
    }
}
