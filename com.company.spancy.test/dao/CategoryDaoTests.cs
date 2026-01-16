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
    public class CategoryDaoTests : BaseTest
    {
        public ICategoryDao CategoryDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.CategoryDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "Java";
            this.CategoryDao.SaveEntity(rootCategory);
            Assert.AreEqual(1, this.CategoryDao.LoadAllEntities().Count);

            IList<Category> categories = this.CategoryDao.LoadAllEntities();
            Category parentCategory = categories[0];
            Category subCategory = new Category();
            subCategory.Name = "JEE";
            subCategory.ParentCategory = parentCategory;
            this.CategoryDao.SaveEntity(subCategory);
            Assert.AreEqual(2, this.CategoryDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "Java";
            this.CategoryDao.SaveEntity(rootCategory);
            Assert.AreEqual(1, this.CategoryDao.LoadAllEntities().Count);

            IList<Category> categories = this.CategoryDao.LoadAllEntities();
            Category parentCategory = categories[0];
            Category subCategory = new Category();
            subCategory.Name = "JEE";
            subCategory.ParentCategory = parentCategory;
            this.CategoryDao.SaveEntity(subCategory);

            categories = this.CategoryDao.LoadAllEntities();
            Category searchCategory = this.CategoryDao.LoadEntity(categories[1].Id);
            Assert.AreEqual("JEE", searchCategory.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "Java";
            this.CategoryDao.SaveEntity(rootCategory);
            Assert.AreEqual(1, this.CategoryDao.LoadAllEntities().Count);

            IList<Category> categories = this.CategoryDao.LoadAllEntities();
            Category parentCategory = categories[0];
            Category subCategory = new Category();
            subCategory.Name = "JEE";
            subCategory.ParentCategory = parentCategory;
            this.CategoryDao.SaveEntity(subCategory);

            categories = this.CategoryDao.LoadAllEntities();
            Category searchCategory = categories[1];
            searchCategory.ParentCategory = null;
            this.CategoryDao.UpdateEntity(searchCategory);
            this.CategoryDao.DeleteEntity(searchCategory);
            Assert.AreEqual(1, this.CategoryDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "Java";
            this.CategoryDao.SaveEntity(rootCategory);

            Category nextCategory = new Category();
            nextCategory.Name = "Ejb 2.1";

            IList<Category> categories = this.CategoryDao.LoadAllEntities();
            rootCategory = categories[0];
            nextCategory.ParentCategory = rootCategory;
            this.CategoryDao.SaveEntity(nextCategory);

            categories = this.CategoryDao.LoadAllEntities();
            Category parentCategory = categories[0];
            Category subCategory = new Category();
            subCategory.Name = "JEE";
            subCategory.ParentCategory = parentCategory;
            this.CategoryDao.SaveEntity(subCategory);

            categories = this.CategoryDao.LoadAllEntities();
            Category searchCategory = categories[0];
            searchCategory.Name = "Spring 3.0";
            rootCategory = categories[2];
            searchCategory.ParentCategory = rootCategory;
            this.CategoryDao.UpdateEntity(searchCategory);

            categories = this.CategoryDao.LoadAllEntities();
            Assert.AreEqual("JEE", categories[0].ParentCategory.Name);
        }
    }
}
