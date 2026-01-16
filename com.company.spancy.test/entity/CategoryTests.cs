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
    public class CategoryTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "Java";
            this.SessionFactory.GetCurrentSession().Save(rootCategory);

            IList<Category> categories = this.SessionFactory.GetCurrentSession().CreateQuery("select category from Category category order by id desc").List<Category>();
            Category parentCategory = categories[0];
            Category subCategory = new Category();
            subCategory.Name = "JEE";
            subCategory.ParentCategory = parentCategory;
            SessionFactory.GetCurrentSession().Save(subCategory);
            
            categories = this.SessionFactory.GetCurrentSession().CreateQuery("select category from Category category order by id desc").List<Category>();
            Assert.AreEqual(2, categories.Count());
            rootCategory = categories[1];
            subCategory = categories[0];
            Assert.AreEqual(rootCategory.Name, subCategory.ParentCategory.Name);
            subCategory.Name = "Ejb 2.1";
            subCategory.ParentCategory = null;
            this.SessionFactory.GetCurrentSession().Merge(subCategory);
            
            categories = this.SessionFactory.GetCurrentSession().CreateQuery("select category from Category category order by id desc").List<Category>();
            Assert.AreEqual(null, categories[0].ParentCategory);
            this.SessionFactory.GetCurrentSession().Delete(subCategory);
            
            categories = this.SessionFactory.GetCurrentSession().CreateQuery("select category from Category category order by id desc").List<Category>();
            Assert.AreEqual(1, categories.Count());
        }
    }
}
