using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class ProductServiceTests : BaseTest
    {
        public IProductService ProductService { get; set; } = null!;

        public ProductServiceTests() : base(true) {}

        [TestMethod]
        public void FindAllTest()
        {
            Assert.IsTrue(this.ProductService.FindAllRO().Count > 0);
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

            Assert.IsTrue(this.ProductService.FindAllRO().Count > 0);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ProductDto sproduct = new ProductDto();
            sproduct.Name = "Spring in Action Book";
            long id = (long) this.ProductService.CreateTX(sproduct);

            IList<ProductDto> pList = this.ProductService.FindAllRO();
            ProductDto product = pList[0];

            ProductDto product2 = this.ProductService.FindByIdRO(id);
            Assert.AreEqual("Spring in Action Book", product2.Name);
        }
    }
}
