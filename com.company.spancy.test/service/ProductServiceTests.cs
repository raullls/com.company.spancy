using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class ProductServiceTests : BaseTest
    {
        public IProductService ProductService { get; set; }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.ProductService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProductDto sproduct = new ProductDto();
            sproduct.Name = "Spring in Action Book";
            
            this.ProductService.CreateProductTX(sproduct);

            ProductDto hproduct = new ProductDto();
            hproduct.Name = "Hibernate in Action Book";
            this.ProductService.CreateTX(hproduct);

            Assert.AreEqual(2, this.ProductService.FindAllRO().Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ProductDto sproduct = new ProductDto();
            sproduct.Name = "Spring in Action Book";
            this.ProductService.CreateTX(sproduct);

            IList<ProductDto> pList = this.ProductService.FindAllRO();
            ProductDto product = pList[0];

            ProductDto product2 = this.ProductService.FindByIdRO(product.Id);
            Assert.AreEqual("Spring in Action Book", product2.Name);
        }
    }
}
