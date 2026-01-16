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
    public class ProductDaoTests : BaseTest
    {
        public IProductDao ProductDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.ProductDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Product sProduct = new Product();
            sProduct.Name = "Spring in Action";
            this.ProductDao.SaveEntity(sProduct);

            Product hProduct = new Product();
            hProduct.Name = "Hibernate in Action";
            this.ProductDao.SaveEntity(hProduct);

            Assert.AreEqual(2, this.ProductDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Product sProduct = new Product();
            sProduct.Name = "Spring in Action";
            this.ProductDao.SaveEntity(sProduct);

            IList<Product> pList = this.ProductDao.LoadAllEntities();
            Product product = pList[0];

            Product product2 = this.ProductDao.LoadEntity(product.Id);
            Assert.AreEqual("Spring in Action", product2.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Product sProduct = new Product();
            sProduct.Name = "Spring in Action";
            this.ProductDao.SaveEntity(sProduct);

            Product hProduct = new Product();
            hProduct.Name = "Hibernate in Action";
            this.ProductDao.SaveEntity(hProduct);

            Assert.AreEqual(2, this.ProductDao.LoadAllEntities().Count);

            this.ProductDao.DeleteEntity(hProduct);

            Assert.AreEqual(1, this.ProductDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Product sProduct = new Product();
            sProduct.Name = "Spring in Action";
            this.ProductDao.SaveEntity(sProduct);

            Assert.AreEqual(1, this.ProductDao.LoadAllEntities().Count);

            IList<Product> pList = this.ProductDao.LoadAllEntities();
            Product product = pList[0];
            product.Name = "Head First Design Patterns";

            this.ProductDao.UpdateEntity(product);

            IList<Product> pList2 = this.ProductDao.LoadAllEntities();
            Product product2 = pList2[0];
            Assert.AreEqual("Head First Design Patterns", product2.Name);
        }
    }
}
