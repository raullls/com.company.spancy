using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class CategoryServiceTests : BaseTest
    {
        public ICategoryService CategoryService { get; set; }
        public CategoryServiceTests() : base(true) { }
        
        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.CategoryService.FindAllRO().Count());
        }

        [TestMethod]
        public void CreateTest()
        {
            CategoryDto parent = new CategoryDto();
            parent.Name = "Java";
            this.CategoryService.CreateCategoryTX(parent);
            Assert.AreEqual(1, this.CategoryService.FindAllRO().Count());
            
            parent = this.CategoryService.FindAllRO()[0];
            CategoryDto jee = new CategoryDto();
            jee.Name = "Java EE";
            jee.ParentId = parent.Id;
            this.CategoryService.CreateCategoryTX(jee);
            Assert.AreEqual(2, this.CategoryService.FindAllRO().Count());
        }

        [TestMethod]
        public void FindByIdTest()
        {
            CategoryDto parent = new CategoryDto();
            parent.Name = "Java";
            this.CategoryService.CreateCategoryTX(parent);

            parent = this.CategoryService.FindAllRO()[0];
            CategoryDto jee = new CategoryDto();
            jee.Name = "Java EE";
            jee.ParentId = parent.Id;
            this.CategoryService.CreateCategoryTX(jee);

            IList<CategoryDto> parentDtos = this.CategoryService.FindAllRO();
            CategoryDto dto = this.CategoryService.FindByIdRO(parentDtos[0].Id);
            Assert.AreEqual(dto.Name, "Java");
        }

        [TestMethod]
        public void RemoveTest()
        {
            CategoryDto parent = new CategoryDto();
            parent.Name = "Java";
            this.CategoryService.CreateCategoryTX(parent);
            Assert.AreEqual(1, this.CategoryService.FindAllRO().Count());
            
            parent = this.CategoryService.FindAllRO()[0];
            CategoryDto jee = new CategoryDto();
            jee.Name = "Java EE";
            jee.ParentId = parent.Id;
            this.CategoryService.CreateCategoryTX(jee);
            Assert.AreEqual(2, this.CategoryService.FindAllRO().Count());
            
            IList<CategoryDto> parentDtos = this.CategoryService.FindAllRO();
            CategoryDto dto = parentDtos[0];
            this.CategoryService.RemoveCategoryByIdTX(dto.Id);
            Assert.AreEqual(1, this.CategoryService.FindAllRO().Count());
        }

        [TestMethod]
        public void EditTest()
        {
            CategoryDto parent = new CategoryDto();
            parent.Name = "Java";
            this.CategoryService.CreateCategoryTX(parent);
            Assert.AreEqual(1, this.CategoryService.FindAllRO().Count());

            parent = this.CategoryService.FindAllRO()[0];
            CategoryDto jee = new CategoryDto();
            jee.Name = "Java EE";
            jee.ParentId = parent.Id;
            this.CategoryService.CreateCategoryTX(jee);
            Assert.AreEqual(2, this.CategoryService.FindAllRO().Count());
            
            IList<CategoryDto> parentDtos = this.CategoryService.FindAllRO();
            CategoryDto dto = parentDtos[1];
            dto.Name = "JEE";
            this.CategoryService.UpdateCategoryTX(dto);
            parentDtos = this.CategoryService.FindAllRO();
            dto = parentDtos[1];
            Assert.AreEqual("JEE", dto.Name);
        }
    }
}
